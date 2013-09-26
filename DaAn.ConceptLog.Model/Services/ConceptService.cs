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
                if (blobDetails.Type == 1)
                {
                    var blob = this.blobRepository.Read(path, blobDetails.BlobId);

                    if (blob == null)
                    {
                        continue;
                    }

                    result.Add(JsonConvert.DeserializeObject<Concept>(blob.Content));
                }
            }

            return result;
        }

        public string Commit(string path, Guid userId, string description, ProjectDetails projectDetails, List<Concept> addedConcepts, List<Concept> editedConcepts, List<Concept> deletedConcepts)
        {
            Commit previousCommit = null;
            if (projectDetails.PreviuosCommitId != null)
            {
                previousCommit = this.commitRepository.Read(path, projectDetails.PreviuosCommitId);
            }

            var newCommit = new Commit()
            {
                Id = ProjectTools.NewId(),
                ParentId = previousCommit == null ? null : previousCommit.Id,
                UserId = userId,
                Description = description
            };

            var newBlobs = new List<Blob>();

            if (previousCommit != null)
            {
                newCommit.BlobsDetails = previousCommit.BlobsDetails.Where(r => !deletedConcepts.Any(c => c.Id.ToString() == r.ObjectId)).ToList();
            }

            foreach (var concept in addedConcepts)
            {
                var blob = ConvertConceptToBlob(concept);

                newBlobs.Add(blob);

                newCommit.BlobsDetails.Add(new BlobDetails()
                {
                    BlobId = blob.Id,
                    ObjectId = concept.Id.ToString(),
                    Type = 1,
                    Action = 1
                });
            }

            foreach (var concept in editedConcepts)
            {
                var blob = ConvertConceptToBlob(concept);

                newBlobs.Add(blob);

                var blobDetails = newCommit.BlobsDetails.SingleOrDefault(r => r.ObjectId == concept.Id.ToString());

                blobDetails.BlobId = blob.Id;
                blobDetails.Action = 2;
            }



            var branch = this.branchRepository.Read(path, projectDetails.BranchName);
            branch.CommitId = newCommit.Id;

            projectDetails.PreviuosCommitId = newCommit.Id;

            this.projectDetailsRepository.Save(path, projectDetails);
            this.branchRepository.Save(path, branch);
            this.commitRepository.Save(path, newCommit);
            this.blobRepository.Save(path, newBlobs);

            addedConcepts.Clear();
            editedConcepts.Clear();
            deletedConcepts.Clear();

            return newCommit.Id;
        }

        private Blob ConvertConceptToBlob(Concept concept)
        {
            return new Blob()
            {
                Id = ProjectTools.NewId(),
                Content = JsonConvert.SerializeObject(concept)
            }; ;
        }
    }
}
