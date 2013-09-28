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
    public class ConceptServiceLocalDeltaDecorator : ConceptServiceDeltaDecorator
    {
        public List<Delta> LocalDeltas { get; set; }

        public ConceptServiceLocalDeltaDecorator(IConceptService conceptService, DeltaRepository deltaRepository) :
            base(conceptService, deltaRepository)
        {
            this.LocalDeltas = new List<Delta>();
        }

        public override List<Delta> GetDeltas()
        {
            var result = base.GetDeltas().ToList();

            result.AddRange(this.LocalDeltas);

            return result;
        }
    }
}
