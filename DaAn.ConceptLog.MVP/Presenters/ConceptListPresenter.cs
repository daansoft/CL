using DaAn.ConceptLog.Model;
using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Services;
using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP.Presenters
{
    public class ConceptListPresenter
    {
        private ConceptServiceWithDeltas conceptService;
        private IConceptListView conceptListView;

        private string commitId;

        public List<string> ConceptIds { get; set; }
        public ActionResult ActionResult { get; set; }

        public ConceptListPresenter(IConceptListView conceptListView, string commitId)
        {
            conceptListView.ConceptListPresenter = this;
            this.conceptListView = conceptListView;

            this.commitId = commitId;

            this.conceptService = ObjectFactory.Instance.GetConceptServiceWithDeltas();
        }

        public void Show()
        {
            this.conceptListView.SetConcepts(this.conceptService.FindByCommitId(this.commitId));

            this.conceptListView.ShowView();
        }

        public void Select()
        {
            var conceptIds = this.conceptListView.GetSelected();

            this.conceptListView.CloseView();

            this.ActionResult = MVP.ActionResult.OK;
            this.ConceptIds = conceptIds;
        }
    }
}
