using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public class BlobDetails
    {
        public string BlobId { get; set; }
        public string ObjectId { get; set; }
        public int Type { get; set; }
        public int Action { get; set; }
    }
}
