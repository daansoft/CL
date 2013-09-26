using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public enum DeltaKey
    {
        AddConcept = 1,
        RemoveConcept = 2,
        UpdateDescription = 3,
        AddRelatedConcept = 4,
        RemoveRelatedConcept = 5,
        AddConceptUser = 6
    }

    public class Delta
    {
        public string ObjectId { get; set; }
        public DeltaKey Key { get; set; }
        public object Value { get; set; }
    }
}
