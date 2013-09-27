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
    public class ConceptServiceDecorator : IConceptService
    {
        private IConceptService conceptService;
        private DeltaRepository deltaRepository;

        public ConceptServiceDecorator(IConceptService conceptService, DeltaRepository deltaRepository)
        {
            this.conceptService = conceptService;
            this.deltaRepository = deltaRepository;
        }

        public List<Concept> FindByBranchName(string path, string branchName)
        {
            return this.deltaRepository.MergeConceptWithDeltas(this.conceptService.FindByBranchName(path, branchName));
        }

        public List<Concept> FindByCommitId(string path, string commitId)
        {
            return this.deltaRepository.MergeConceptWithDeltas(this.conceptService.FindByCommitId(path, commitId));
        }

        public Concept ReadConceptFromBlobDetails(string path, BlobDetails blobDetails)
        {
            return this.deltaRepository.MergeConceptWithDelta(this.conceptService.ReadConceptFromBlobDetails(path, blobDetails));
        }

        public List<Concept> FindRelatedConceptsByCommitIdAndConceptId(string path, string commitId, string conceptId)
        {
            var concepts = this.conceptService.FindRelatedConceptsByCommitIdAndConceptId(path, commitId, conceptId);

            //TODO

            return concepts;
        }

        public Concept ReadConceptByCommitIdAndConceptId(string path, string currentCommitId, string conceptId)
        {
            var concept = this.conceptService.ReadConceptByCommitIdAndConceptId(path, currentCommitId, conceptId);

            if (concept == null)
            {
                return null; //TODO
            }

            return concept;
        }
    }
}
