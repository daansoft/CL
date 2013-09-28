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

        public Concept Clone()
        {
            var concept = new Concept();

            concept.Id = this.Id;
            concept.Description = this.Description;
            concept.CreateDate = this.CreateDate;
            concept.Action = this.Action;
            concept.CreatorId = this.CreatorId;
            concept.RelatedConceptIds = this.RelatedConceptIds.ToList();
            concept.UserIds = this.UserIds.ToList();

            return concept;
        }
    }
}
