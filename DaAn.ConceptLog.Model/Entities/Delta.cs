using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public enum DeltaKey
    {
        Concept = 1,
        ConceptDescription = 2,
        RelatedConcept = 3,
        ConceptUser = 4
    }

    public enum DeltaAction
    {
        Add = 1,
        Update = 2,
        Remove = 3
    }

    public class Delta
    {
        public string ObjectId { get; set; }
        public DeltaKey Key { get; set; }
        public object Value { get; set; }
        public DeltaAction Action { get; set; }
    }
}
