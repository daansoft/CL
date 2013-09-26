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
        private SnapshotRepository snapshotRepository;
        private BlobRepository blobRepository;

        public ConceptService(ProjectDetailsRepository projectDetailsRepository, BranchRepository branchRepository, CommitRepository commitRepository, SnapshotRepository snapshotRepository, BlobRepository blobRepository)
        {
            this.projectDetailsRepository = projectDetailsRepository;
            this.branchRepository = branchRepository;
            this.commitRepository = commitRepository;
            this.snapshotRepository = snapshotRepository;
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
            var commit = this.commitRepository.Read(path, commitId);

            if (commit == null)
            {
                return new List<Concept>();
            }

            return this.FindBySnapshotId(path, commit.SnapshotId);
        }

        public List<Concept> FindBySnapshotId(string path, string snapshotId)
        {
            var result = new List<Concept>();

            var snapshot = this.snapshotRepository.Read(path, snapshotId);

            if (snapshot == null)
            {
                return new List<Concept>();
            }

            foreach (var blobDetails in snapshot.BlobsDetails)
            {
                var blob = this.blobRepository.Read(path, blobDetails.BlobId);

                if (blob == null)
                {
                    continue;
                }

                result.Add(JsonConvert.DeserializeObject<Concept>(blob.Content));
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

            Snapshot previousSnapshot = null;
            if (previousCommit != null)
            {
                previousSnapshot = snapshotRepository.Read(path, previousCommit.SnapshotId);
            }

            var newSnapshot = new Snapshot()
            {
                Id = ProjectTools.NewId()
            };

            var newBlobs = new List<Blob>();

            if (previousSnapshot != null)
            {
                newSnapshot.BlobsDetails = previousSnapshot.BlobsDetails.Where(r => !deletedConcepts.Any(c => c.Id == r.ConceptId)).ToList();
            }

            foreach (var concept in addedConcepts)
            {
                var blob = ConvertConceptToBlob(concept);

                newBlobs.Add(blob);

                newSnapshot.BlobsDetails.Add(new BlobDetails()
                {
                    BlobId = blob.Id,
                    ConceptId = concept.Id
                });
            }

            foreach (var concept in editedConcepts)
            {
                var blob = ConvertConceptToBlob(concept);

                newBlobs.Add(blob);

                var blobDetails = newSnapshot.BlobsDetails.SingleOrDefault(r => r.ConceptId == concept.Id);

                blobDetails.BlobId = blob.Id;
            }

            var newCommit = new Commit()
            {
                Id = ProjectTools.NewId(),
                ParentId = previousCommit == null ? null : previousCommit.Id,
                SnapshotId = newSnapshot.Id,
                UserId = userId,
                Description = description
            };

            var branch = this.branchRepository.Read(path, projectDetails.BranchName);
            branch.CommitId = newCommit.Id;

            projectDetails.PreviuosCommitId = newCommit.Id;

            this.projectDetailsRepository.Save(path, projectDetails);
            this.branchRepository.Save(path, branch);
            this.commitRepository.Save(path, newCommit);
            this.snapshotRepository.Save(path, newSnapshot);
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
