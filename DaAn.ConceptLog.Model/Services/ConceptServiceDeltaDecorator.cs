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
    public class ConceptServiceDeltaDecorator : IConceptService
    {
        private IConceptService conceptService;
        private DeltaRepository deltaRepository;

        public ConceptServiceDeltaDecorator(IConceptService conceptService, DeltaRepository deltaRepository)
        {
            this.conceptService = conceptService;
            this.deltaRepository = deltaRepository;
        }

        public List<Concept> FindByBranchName(string path, string branchName)
        {
            return this.deltaRepository.MergeConceptWithDeltas(this.conceptService.FindByBranchName(path, branchName), this.GetDeltas());
        }

        public List<Concept> FindByCommitId(string path, string commitId)
        {
            return this.deltaRepository.MergeConceptWithDeltas(this.conceptService.FindByCommitId(path, commitId), this.GetDeltas());
        }

        public Concept ReadConceptFromBlobDetails(string path, BlobDetails blobDetails)
        {
            return this.deltaRepository.MergeConceptWithDeltas(this.conceptService.ReadConceptFromBlobDetails(path, blobDetails), this.GetDeltas());
        }

        public List<Concept> FindRelatedConceptsByCommitIdAndConceptId(string path, string commitId, string conceptId)
        {
            var concepts = this.deltaRepository.MergeConceptWithDeltas(this.conceptService.FindRelatedConceptsByCommitIdAndConceptId(path, commitId, conceptId), this.GetDeltas());

            var relatedConceptIds = this.deltaRepository.FindRelatedConceptsIdsByConceptId(conceptId, this.GetDeltas());

            foreach (var relatedConceptId in relatedConceptIds)
            {
                var relatedConcept = this.ReadConceptByCommitIdAndConceptId(path, commitId, relatedConceptId);

                if (relatedConcept != null)
                {
                    concepts.Add(relatedConcept);
                }
            }

            return concepts;
        }

        public Concept ReadConceptByCommitIdAndConceptId(string path, string currentCommitId, string conceptId)
        {
            var concept = this.conceptService.ReadConceptByCommitIdAndConceptId(path, currentCommitId, conceptId);

            if (concept == null)
            {
                concept = this.deltaRepository.ReadConceptByConceptId(conceptId, this.GetDeltas());
            }

            return this.deltaRepository.MergeConceptWithDeltas(concept, this.GetDeltas());
        }

        public virtual List<Delta> GetDeltas()
        {
            return this.deltaRepository.FindAll();
        }
    }
}
