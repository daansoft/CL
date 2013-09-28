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
    public class DeltaService
    {
        private DeltaRepository deltaRepository;

        public DeltaService(DeltaRepository deltaRepository)
        {
            this.deltaRepository = deltaRepository;
        }

        public void Create(List<Delta> deltas)
        {
            this.deltaRepository.Create(deltas);
        }

        public void Create(Delta delta)
        {
            this.deltaRepository.Create(delta);
        }

        public void DeleteAll()
        {
            this.deltaRepository.DeleteAll();
        }

        public List<Concept> MergeConceptWithDeltas(List<Concept> concepts, List<Delta> deltas)
        {
            return this.deltaRepository.MergeConceptWithDeltas(concepts, deltas);
        }

        public Concept MergeConceptWithDeltas(Concept concept, List<Delta> deltas)
        {
            return this.deltaRepository.MergeConceptWithDeltas(concept, deltas);
        }
    }
}
