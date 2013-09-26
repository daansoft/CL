using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public class Delta
    {
        public string ObjectId { get; set; }

        public int Action { get; set; } // add, update, remove
    }
}
