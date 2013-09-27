using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public class Concept
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatorId { get; set; }

        public List<string> RelatedConceptIds;
        public List<Guid> UserIds;

        public CommitAction Action { get; set; }

        public Concept()
        {
            this.Action = CommitAction.Create;
            this.RelatedConceptIds = new List<string>();
            this.UserIds = new List<Guid>();
        }
    }
}
