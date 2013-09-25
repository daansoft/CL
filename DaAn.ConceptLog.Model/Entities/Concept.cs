using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public class Concept
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }
        public Guid Creator { get; set; }

        public List<Guid> RelatedConceptIds;

        public List<Guid> UserIds;
    }
}
