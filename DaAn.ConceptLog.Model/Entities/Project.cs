using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public class Project
    {
        public string Path { get; set; }
        public ProjectDetails Details { get; set; }
        public List<Branch> Braches { get; set; }
        public List<Commit> Commits { get; set; }
    }
}
