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

        public ConceptPresenter GetConceptPresenter(string conceptId)
        {
            return new ConceptPresenter(this.ViewFactory.GetConceptView(), conceptId);
        }
    }
}
