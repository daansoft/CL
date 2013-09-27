using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Repositories;
using DaAn.ConceptLog.Model.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Services
{
    public class ConceptService
    {
        private ProjectDetailsRepository projectDetailsRepository;
        private BranchRepository branchRepository;
        private CommitRepository commitRepository;
        private BlobRepository blobRepository;

        public ConceptService(ProjectDetailsRepository projectDetailsRepository, BranchRepository branchRepository, CommitRepository commitRepository, BlobRepository blobRepository)
        {
            this.projectDetailsRepository = projectDetailsRepository;
            this.branchRepository = branchRepository;
            this.commitRepository = commitRepository;
            this.blobRepository = blobRepository;
        }

        public List<Concept> FindByBranchName(string path, string branchName, List<Delta> deltas)
        {
            var branch = this.branchRepository.Read(path, branchName);

            if (branch == null)
            {
                return this.MergeConceptWithDeltas(new List<Concept>(), deltas); //TODO
            }

            return this.MergeConceptWithDeltas(this.FindByCommitId(path, branch.CommitId, null), deltas);
        }

        public List<Concept> FindByCommitId(string path, string commitId, List<Delta> deltas)
        {
            var result = new List<Concept>();

            var commit = this.commitRepository.Read(path, commitId);

            if (commit != null)
            {
                foreach (var blobDetails in commit.BlobsDetails)
                {
                    var concept = this.ReadConceptFromBlobDetails(path, blobDetails, null);
                    if (concept != null)
                    {
                        result.Add(concept);
                    }
                }
            }

            return this.MergeConceptWithDeltas(result, deltas);
        }

        private Concept ReadConceptFromBlobDetails(string path, BlobDetails blobDetails, List<Delta> deltas)
        {
            if (blobDetails.Type != BlobDetailsType.Concept)
            {
                return null; //TODO ?
            }

            var blob = this.blobRepository.Read(path, blobDetails.BlobId);
            return this.MergeConceptWithDelta(this.ReadConceptFromBlob(blob, null), deltas);
        }

        private Concept ReadConceptFromBlob(Blob blob, List<Delta> deltas)
        {
            var concept = JsonConvert.DeserializeObject<Concept>(blob.Content);
            return this.MergeConceptWithDelta(concept, deltas);
        }

        public void Commit(string path, Guid userId, string description, ProjectDetails projectDetails, List<Delta> deltas)
        {
            var concepts = new List<Concept>();
            Commit previousCommit = null;
            if (projectDetails.PreviuosCommitId != null)
            {
                previousCommit = this.commitRepository.Read(path, projectDetails.PreviuosCommitId);

                //TODO spraedzić czy pobierać po branch
                concepts = this.FindByCommitId(path, projectDetails.PreviuosCommitId, deltas);
            }

            var newCommit = new Commit()
            {
                Id = ProjectTools.NewId(),
                ParentId = previousCommit == null ? null : previousCommit.Id,
                UserId = userId,
                Description = description
            };

            var newBlobs = new List<Blob>();

            foreach (var concept in concepts)
            {
                BlobDetails blobDetails = null;
                if (previousCommit != null)
                {
                    blobDetails = previousCommit.BlobsDetails.SingleOrDefault(r => r.ObjectId == concept.Id);
                }

                switch (concept.Action)
                {
                    case CommitAction.NoChange:
                        newCommit.BlobsDetails.Add(blobDetails);
                        break;
                    case CommitAction.Create:
                        var blob = this.ConvertConceptToBlob(concept);

                        newBlobs.Add(blob);

                        newCommit.BlobsDetails.Add(new BlobDetails()
                        {
                            BlobId = blob.Id,
                            ObjectId = concept.Id,
                            Type = BlobDetailsType.Concept
                        });
                        break;
                    case CommitAction.Delete:
                        // don't add blobDetails
                        break;
                    case CommitAction.Update:
                        blob = this.ConvertConceptToBlob(concept);

                        newBlobs.Add(blob);

                        blobDetails.BlobId = blob.Id;

                        newCommit.BlobsDetails.Add(blobDetails);
                        break;
                }

            }

            projectDetails.PreviuosCommitId = newCommit.Id;

            var branch = this.branchRepository.Read(path, projectDetails.BranchName);
            branch.CommitId = newCommit.Id;

            this.projectDetailsRepository.Save(path, projectDetails);
            this.branchRepository.Save(path, branch);
            this.commitRepository.Save(path, newCommit);
            this.blobRepository.Save(path, newBlobs);

            deltas.Clear();
        }

        private Blob ConvertConceptToBlob(Concept concept)
        {
            return new Blob()
            {
                Id = ProjectTools.NewId(),
                Content = JsonConvert.SerializeObject(concept)
            };
        }



        public Concept MergeConceptWithDelta(Concept concept, List<Delta> deltas)
        {
            if (deltas == null)
            {
                return concept;
            }

            var deltasForConcept = deltas.Where(r => r.ObjectId == concept.Id).ToList();

            foreach (var delta in deltasForConcept)
            {
                switch (delta.Action)
                {
                    case DeltaAction.AddConcept:
                        /*var newConcept = (Concept)delta.Value;
                        if (newConcept != null)
                        {
                            result.Add(newConcept);
                            newConcept.Action = CommitAction.Create;
                        }*/
                        break;
                    case DeltaAction.AddRelatedConcept:
                        if (concept != null)
                        {
                            concept.RelatedConceptIds.Add((string)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.AddConceptUser:
                        if (concept != null)
                        {
                            concept.UserIds.Add((Guid)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.UpdateConceptDescription:
                        if (concept != null)
                        {
                            concept.Description = (string)delta.Value;
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.RemoveConcept:
                        if (concept != null)
                        {
                            //result.Remove(concept);

                            concept = null;
                        }
                        break;
                    case DeltaAction.RemoveRelatedConcept:
                        if (concept != null)
                        {
                            concept.RelatedConceptIds.Remove((string)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.RemoveConceptUser:
                        if (concept != null)
                        {
                            concept.UserIds.Remove((Guid)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    default:
                        break;
                }
            }

            return concept;
        }

        public List<Concept> MergeConceptWithDeltas(List<Concept> concepts, List<Delta> deltas)
        {
            var result = concepts.ToList();

            if (deltas == null)
            {
                return result;
            }

            foreach (var delta in deltas.OrderBy(r => (int)r.Action))
            {
                var concept = result.SingleOrDefault(r => r.Id == delta.ObjectId);

                switch (delta.Action)
                {
                    case DeltaAction.AddConcept:
                        var newConcept = (Concept)delta.Value;
                        if (newConcept != null)
                        {
                            result.Add(newConcept);
                            newConcept.Action = CommitAction.Create;
                        }
                        break;
                    case DeltaAction.AddRelatedConcept:
                        if (concept != null)
                        {
                            concept.RelatedConceptIds.Add((string)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.AddConceptUser:
                        if (concept != null)
                        {
                            concept.UserIds.Add((Guid)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.UpdateConceptDescription:
                        if (concept != null)
                        {
                            concept.Description = (string)delta.Value;
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.RemoveConcept:
                        if (concept != null)
                        {
                            result.Remove(concept);
                        }
                        break;
                    case DeltaAction.RemoveRelatedConcept:
                        if (concept != null)
                        {
                            concept.RelatedConceptIds.Remove((string)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaAction.RemoveConceptUser:
                        if (concept != null)
                        {
                            concept.UserIds.Remove((Guid)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        public List<Concept> FindRelatedConceptsByCommitIdAndConceptId(string path, string commitId, string conceptId, List<Delta> deltas)
        {
            var concept = this.ReadConceptByCommitIdAndConceptId(path, commitId, conceptId, deltas);

            if (concept == null)
            {
                return null; //TODO
            }

            var result = new List<Concept>();

            foreach (var relatedConceptId in concept.RelatedConceptIds)
            {
                var relatedConcept = this.ReadConceptByCommitIdAndConceptId(path, commitId, relatedConceptId, deltas);

                if (relatedConcept != null)
                {
                    result.Add(relatedConcept);
                }
            }

            return result;
        }

        public Concept ReadConceptByCommitIdAndConceptId(string path, string currentCommitId, string conceptId, List<Delta> deltas)
        {
            var commit = this.commitRepository.Read(path, currentCommitId);

            if (commit == null)
            {
                return null; //TODO
            }

            var blobDetails = commit.BlobsDetails.SingleOrDefault(r => r.ObjectId == conceptId);

            if (blobDetails == null)
            {
                var concepts = new List<Concept>();

                concepts = this.MergeConceptWithDeltas(concepts, deltas);
            }

            return this.ReadConceptFromBlobDetails(path, blobDetails, deltas);
        }
    }
}
