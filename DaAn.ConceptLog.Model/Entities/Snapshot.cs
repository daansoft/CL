using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public class Snapshot
    {
        public string Id { get; set; }
        public List<BlobDetails> BlobsDetails { get; set; }

        public Snapshot()
        {
            this.BlobsDetails = new List<BlobDetails>();
        }
    }
}
