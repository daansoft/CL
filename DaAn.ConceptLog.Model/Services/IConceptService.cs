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
        List<Concept> FindByBranchName(string path, string branchName, List<Delta> deltas);
        List<Concept> FindByCommitId(string path, string commitId, List<Delta> deltas);
        Concept ReadConceptFromBlobDetails(string path, BlobDetails blobDetails, List<Delta> deltas);
        void Commit(string path, Guid userId, string description, ProjectDetails projectDetails, List<Delta> deltas);
        Concept MergeConceptWithDelta(Concept concept, List<Delta> deltas);
        List<Concept> MergeConceptWithDeltas(List<Concept> concepts, List<Delta> deltas);
        List<Concept> FindRelatedConceptsByCommitIdAndConceptId(string path, string commitId, string conceptId, List<Delta> deltas);
        Concept ReadConceptByCommitIdAndConceptId(string path, string currentCommitId, string conceptId, List<Delta> deltas);
    }
}
