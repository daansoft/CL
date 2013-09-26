using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.MVP.Presenters;
using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP
{
    public abstract class MVPFactory
    {
        public abstract IMainView GetMainView();
        public abstract ICommitView GetCommitView();

        public MainPresenter GetMainPresenter()
        {
            return new MainPresenter(this.GetMainView());
        }

        public CommitPresenter GetCommitPresenter(string path, Guid userId, ProjectDetails projectDetails, List<Concept> addedConcepts, List<Concept> editedConcepts, List<Concept> deletedConcepts)
        {
            return new CommitPresenter(this.GetCommitView(), path, userId, projectDetails, addedConcepts, editedConcepts, deletedConcepts);
        }
    }
}
