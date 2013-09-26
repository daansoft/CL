using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public enum BlobDetailsType
    {
        Concept = 1,
    }

    public class BlobDetails
    {
        public string BlobId { get; set; }
        public string ObjectId { get; set; }
        public BlobDetailsType Type { get; set; }
    }
}
