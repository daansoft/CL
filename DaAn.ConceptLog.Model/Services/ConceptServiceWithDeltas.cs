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
    public class ConceptServiceWithDeltas : IConceptService
    {
        private IConceptService conceptService;
        private DeltaRepository deltaRepository;

        public ConceptServiceWithDeltas(IConceptService conceptService, DeltaRepository deltaRepository)
        {
            this.conceptService = conceptService;
            this.deltaRepository = deltaRepository;
        }

        public List<Concept> FindByBranchName(string branchName)
        {
            return this.deltaRepository.MergeConceptWithDeltas(this.conceptService.FindByBranchName(branchName), this.GetDeltas());
        }

        public List<Concept> FindByCommitId(string commitId)
        {
            return this.deltaRepository.MergeConceptWithDeltas(this.conceptService.FindByCommitId(commitId), this.GetDeltas());
        }

        public Concept ReadConceptFromBlobDetails(BlobDetails blobDetails)
        {
            return this.deltaRepository.MergeConceptWithDeltas(this.conceptService.ReadConceptFromBlobDetails(blobDetails), this.GetDeltas());
        }

        public List<Concept> FindRelatedConceptsByCommitIdAndConceptId(string commitId, string conceptId)
        {
            var concepts = this.deltaRepository.MergeConceptWithDeltas(this.conceptService.FindRelatedConceptsByCommitIdAndConceptId(commitId, conceptId), this.GetDeltas().Where(r => r.ObjectId == conceptId && (r.Action == DeltaAction.AddRelatedConcept || r.Action == DeltaAction.RemoveRelatedConcept)).ToList());

            var relatedConceptIds = this.deltaRepository.FindRelatedConceptsIdsByConceptId(conceptId, this.GetDeltas());

            foreach (var relatedConceptId in relatedConceptIds)
            {
                var relatedConcept = this.ReadConceptByCommitIdAndConceptId(commitId, relatedConceptId);

                if (relatedConcept != null)
                {
                    concepts.Add(relatedConcept);
                }
            }

            return concepts;
        }

        public Concept ReadConceptByCommitIdAndConceptId(string currentCommitId, string conceptId)
        {
            var concept = this.conceptService.ReadConceptByCommitIdAndConceptId(currentCommitId, conceptId);

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
