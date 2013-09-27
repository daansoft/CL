using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public enum DeltaAction
    {
        // Add 1xx, Update 2xx, Remove 3xx
        AddConcept = 100,
        AddRelatedConcept = 101,
        AddConceptUser = 102,
        UpdateConceptDescription = 200,
        RemoveRelatedConcept = 300,
        RemoveConcept = 301,
        RemoveConceptUser = 302
    }

    public class Delta
    {
        public string ObjectId { get; set; }
        public object Value { get; set; }
        public DeltaAction Action { get; set; }
    }
}
