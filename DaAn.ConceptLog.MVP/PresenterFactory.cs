using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Services;
using DaAn.ConceptLog.MVP.Presenters;
using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP
{
    public class PresenterFactory
    {
        public IViewFactory ViewFactory { get; set; }

        public MainPresenter GetMainPresenter()
        {
            return new MainPresenter(this.ViewFactory.GetMainView());
        }

        public CommitPresenter GetCommitPresenter(string path, Guid userId, ProjectDetails projectDetails, List<Delta> deltas)
        {
            return new CommitPresenter(this.ViewFactory.GetCommitView(), path, userId, projectDetails, deltas);
        }

        public CreateConceptPresenter GetCreateConceptPresenter(ConceptService conceptService, string path, string commitId, List<Delta> deltas)
        {
            return new CreateConceptPresenter(this.ViewFactory.GetConceptView(), conceptService, path, commitId, deltas);
        }
    }
}
