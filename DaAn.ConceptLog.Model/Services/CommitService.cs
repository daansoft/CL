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
    public class CommitService
    {
        private ProjectDetailsRepository projectDetailsRepository;
        private BranchRepository branchRepository;
        private CommitRepository commitRepository;
        private BlobRepository blobRepository;
        private DeltaRepository deltaRepository;

        public CommitService(ProjectDetailsRepository projectDetailsRepository, BranchRepository branchRepository, CommitRepository commitRepository, BlobRepository blobRepository, DeltaRepository deltaRepository)
        {
            this.projectDetailsRepository = projectDetailsRepository;
            this.branchRepository = branchRepository;
            this.commitRepository = commitRepository;
            this.blobRepository = blobRepository;
            this.deltaRepository = deltaRepository;
        }

        public void Commit(string path, Guid userId, string description, ProjectDetails projectDetails, List<Concept> concepts)
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

            this.deltaRepository.DeleteAll();
        }

        private Blob ConvertConceptToBlob(Concept concept)
        {
            return new Blob()
            {
                Id = ProjectTools.NewId(),
                Content = JsonConvert.SerializeObject(concept)
            };
        }
    }
}
