using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Repositories;
using DaAn.ConceptLog.Model.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Services
{
    public class ProjectService
    {
        private CommitRepository commitRepository;
        private SnapshotRepository snapshotRepository;
        private BlobRepository blobRepository;

        public ProjectService(CommitRepository commitRepository, SnapshotRepository snapshotRepository, BlobRepository blobRepository)
        {
            this.commitRepository = commitRepository;
            this.snapshotRepository = snapshotRepository;
            this.blobRepository = blobRepository;
        }

        public string Commit(string path, Guid userId, string description, string previousCommitId, List<Concept> addedConcepts, List<Concept> editedConcepts, List<Concept> deletedConcepts)
        {
            Commit previousCommit = null;
            if (previousCommitId != null)
            {
                previousCommit = this.commitRepository.Read(path, previousCommitId);
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
                var blob = new Blob()
                {
                    Id = ProjectTools.NewId(),
                    Content = JsonConvert.SerializeObject(concept)
                };

                newBlobs.Add(blob);

                newSnapshot.BlobsDetails.Add(new BlobDetails()
                {
                    Id = blob.Id,
                    ConceptId = concept.Id
                });
            }

            foreach (var concept in editedConcepts)
            {
                var blob = new Blob()
                {
                    Id = ProjectTools.NewId(),
                    Content = JsonConvert.SerializeObject(concept)
                };

                newBlobs.Add(blob);

                var blobDetails = newSnapshot.BlobsDetails.SingleOrDefault(r => r.ConceptId == concept.Id);

                blobDetails.Id = blob.Id;
            }

            var newCommit = new Commit()
            {
                Id = ProjectTools.NewId(),
                ParentId = previousCommit == null ? null : previousCommit.Id,
                SnapshotId = newSnapshot.Id,
                UserId = userId,
                Description = description
            };

            this.commitRepository.Save(path, newCommit);
            this.snapshotRepository.Save(path, newSnapshot);
            this.blobRepository.Save(path, newBlobs);

            return newCommit.Id;
        }
    }
}
