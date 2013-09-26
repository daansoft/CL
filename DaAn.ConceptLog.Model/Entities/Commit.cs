﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public class Commit
    {
        public Commit()
        {
            this.BlobsDetails = new List<BlobDetails>();
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string ParentId { get; set; }
        public List<BlobDetails> BlobsDetails { get; set; }
    }
}
