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
    public class ConceptService : IConceptService
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

        public List<Concept> FindByBranchName(string branchName)
        {
            var branch = this.branchRepository.Read(branchName);

            if (branch == null)
            {
                return new List<Concept>(); //TODO
            }

            return this.FindByCommitId(branch.CommitId);
        }

        public List<Concept> FindByCommitId(string commitId)
        {
            var result = new List<Concept>();

            var commit = this.commitRepository.Read(commitId);

            if (commit != null)
            {
                foreach (var blobDetails in commit.BlobsDetails)
                {
                    var concept = this.ReadConceptFromBlobDetails(blobDetails);
                    if (concept != null)
                    {
                        result.Add(concept);
                    }
                }
            }

            return result;
        }

        public Concept ReadConceptFromBlobDetails(BlobDetails blobDetails)
        {
            if (blobDetails.Type != BlobDetailsType.Concept)
            {
                return null; //TODO ?
            }

            var blob = this.blobRepository.Read(blobDetails.BlobId);
            return this.ReadConceptFromBlob(blob);
        }

        private Concept ReadConceptFromBlob(Blob blob)
        {
            var concept = JsonConvert.DeserializeObject<Concept>(blob.Content);
            concept.Action = CommitAction.NoChange;
            return concept;
        }

        public List<Concept> FindRelatedConceptsByCommitIdAndConceptId(string commitId, string conceptId)
        {
            var concept = this.ReadConceptByCommitIdAndConceptId(commitId, conceptId);

            if (concept == null)
            {
                return new List<Concept>();
            }

            var result = new List<Concept>();

            foreach (var relatedConceptId in concept.RelatedConceptIds)
            {
                var relatedConcept = this.ReadConceptByCommitIdAndConceptId(commitId, relatedConceptId);

                if (relatedConcept != null)
                {
                    result.Add(relatedConcept);
                }
            }

            return result;
        }

        public Concept ReadConceptByCommitIdAndConceptId(string currentCommitId, string conceptId)
        {
            var commit = this.commitRepository.Read(currentCommitId);

            if (commit == null)
            {
                return null; //TODO
            }

            var blobDetails = commit.BlobsDetails.SingleOrDefault(r => r.ObjectId == conceptId);

            if (blobDetails == null)
            {
                return null;
            }

            return this.ReadConceptFromBlobDetails(blobDetails);
        }
    }
}
