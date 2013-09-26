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

        public List<Concept> FindByBranchName(string path, string branchName)
        {
            var branch = this.branchRepository.Read(path, branchName);

            if (branch == null)
            {
                return new List<Concept>();
            }

            return this.FindByCommitId(path, branch.CommitId);
        }

        public List<Concept> FindByCommitId(string path, string commitId)
        {
            var result = new List<Concept>();

            var commit = this.commitRepository.Read(path, commitId);

            if (commit == null)
            {
                return new List<Concept>();
            }

            foreach (var blobDetails in commit.BlobsDetails)
            {
                if (blobDetails.Type == BlobDetailsType.Concept)
                {
                    var blob = this.blobRepository.Read(path, blobDetails.BlobId);

                    if (blob == null)
                    {
                        continue;
                    }

                    var concept = JsonConvert.DeserializeObject<Concept>(blob.Content);
                    concept.Action = CommitAction.NoChange;

                    result.Add(concept);
                }
            }

            return result;
        }

        public string Commit(string path, Guid userId, string description, ProjectDetails projectDetails, List<Delta> deltas)
        {
            var concepts = new List<Concept>();
            Commit previousCommit = null;
            if (projectDetails.PreviuosCommitId != null)
            {
                previousCommit = this.commitRepository.Read(path, projectDetails.PreviuosCommitId);

                //TODO spraedzić czy pobierać po branch
                concepts = this.FindByCommitId(path, projectDetails.PreviuosCommitId);
            }

            var newCommit = new Commit()
            {
                Id = ProjectTools.NewId(),
                ParentId = previousCommit == null ? null : previousCommit.Id,
                UserId = userId,
                Description = description
            };

            var newBlobs = new List<Blob>();

            concepts = this.PrepareConcepts(concepts, deltas);

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
                        var blob = ConvertConceptToBlob(concept);

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
                        blob = ConvertConceptToBlob(concept);

                        newBlobs.Add(blob);

                        blobDetails.BlobId = blob.Id;

                        newCommit.BlobsDetails.Add(blobDetails);
                        break;
                }

            }

            var branch = this.branchRepository.Read(path, projectDetails.BranchName);
            branch.CommitId = newCommit.Id;

            projectDetails.PreviuosCommitId = newCommit.Id;

            this.projectDetailsRepository.Save(path, projectDetails);
            this.branchRepository.Save(path, branch);
            this.commitRepository.Save(path, newCommit);
            this.blobRepository.Save(path, newBlobs);

            deltas.Clear();

            return newCommit.Id;
        }

        private Blob ConvertConceptToBlob(Concept concept)
        {
            return new Blob()
            {
                Id = ProjectTools.NewId(),
                Content = JsonConvert.SerializeObject(concept)
            };
        }

        public List<Concept> PrepareConcepts(List<Concept> concepts, List<Delta> deltas)
        {
            var result = concepts.ToList();

            foreach (var delta in deltas)
            {
                switch (delta.Key)
                {
                    case DeltaKey.AddConcept:
                        var concept = (Concept)delta.Value;
                        if (concept != null)
                        {
                            result.Add(concept);
                            concept.Action = CommitAction.Create;
                        }
                        break;
                    case DeltaKey.RemoveConcept:
                        concept = result.SingleOrDefault(r => r.Id == delta.ObjectId);
                        if (concept != null)
                        {
                            result.Remove(concept);
                        }
                        break;
                    case DeltaKey.UpdateDescription:
                        concept = result.SingleOrDefault(r => r.Id == delta.ObjectId);
                        if (concept != null)
                        {
                            concept.Description = (string)delta.Value;
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaKey.AddRelatedConcept:
                        concept = result.SingleOrDefault(r => r.Id == delta.ObjectId);
                        if (concept != null)
                        {
                            concept.RelatedConceptIds.Add((string)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaKey.RemoveRelatedConcept:
                        concept = result.SingleOrDefault(r => r.Id == delta.ObjectId);
                        if (concept != null)
                        {
                            concept.RelatedConceptIds.Remove((string)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                    case DeltaKey.AddConceptUser:
                        concept = result.SingleOrDefault(r => r.Id == delta.ObjectId);
                        if (concept != null)
                        {
                            concept.UserIds.Add((Guid)delta.Value);
                            concept.Action = CommitAction.Update;
                        }
                        break;
                }

            }

            return result;
        }
    }
}
