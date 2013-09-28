using DaAn.ConceptLog.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model
{
    public class DeltaFactory
    {
        public static DeltaFactory Instance { get; set; }

        static DeltaFactory()
        {
            DeltaFactory.Instance = new DeltaFactory();
        }

        protected DeltaFactory()
        {
        }

        public Delta AddConcept(string conceptId)
        {
            return new Delta()
            {
                ObjectId = conceptId,
                Value = conceptId,
                Action = DeltaAction.AddConcept
            };
        }

        public Delta UpdateConceptDescription(string conceptId, string description)
        {
            return new Delta()
            {
                ObjectId = conceptId,
                Value = description,
                Action = DeltaAction.UpdateConceptDescription
            };
        }
    }
}
