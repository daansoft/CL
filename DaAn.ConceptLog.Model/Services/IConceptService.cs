using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Repositories;
using DaAn.ConceptLog.Model.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Services
{
    public interface IConceptService
    {
        List<Concept> FindByBranchName(string branchName);
        List<Concept> FindByCommitId(string commitId);
        Concept ReadConceptFromBlobDetails(BlobDetails blobDetails);
        List<Concept> FindRelatedConceptsByCommitIdAndConceptId(string commitId, string conceptId);
        Concept ReadConceptByCommitIdAndConceptId(string currentCommitId, string conceptId);
    }
}
