using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public class ProjectDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string BranchName { get; set; }
        public string PreviuosCommitId { get; set; }
    }
}
