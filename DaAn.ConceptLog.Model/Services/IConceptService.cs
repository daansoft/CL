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
        List<Concept> FindByBranchName(string path, string branchName);
        List<Concept> FindByCommitId(string path, string commitId);
        Concept ReadConceptFromBlobDetails(string path, BlobDetails blobDetails);
        List<Concept> FindRelatedConceptsByCommitIdAndConceptId(string path, string commitId, string conceptId);
        Concept ReadConceptByCommitIdAndConceptId(string path, string currentCommitId, string conceptId);
    }
}
