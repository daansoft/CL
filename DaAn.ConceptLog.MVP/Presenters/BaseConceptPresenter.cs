using DaAn.ConceptLog.Model;
using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Services;
using DaAn.ConceptLog.Model.Utils;
using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP.Presenters
{
    public class BaseConceptPresenter
    {
        protected ConceptServiceWithGlobalAndLocalDeltas conceptService;
        protected DeltaService deltaService;

        protected IConceptView conceptView;

        protected string commitId;
        protected string conceptId;

        public BaseConceptPresenter(IConceptView conceptView, string commitId, string conceptId)
        {
            conceptView.ConceptPresenter = this;
            this.conceptView = conceptView;

            this.conceptService = ObjectFactory.Instance.GetConceptServiceWithGlobalAndLocalDeltas();
            this.deltaService = ObjectFactory.Instance.GetDeltaService();

            this.commitId = commitId;

            this.conceptId = conceptId;
        }

        public ActionResult ActionResult { get; set; }

        public virtual void Show()
        {
            this.RefreshData();
            this.conceptView.ShowView();
        }

        protected void RefreshData()
        {
            var concept = this.conceptService.ReadConceptByCommitIdAndConceptId(this.commitId, this.conceptId);

            if (concept != null)
            {
                this.conceptView.Description = concept.Description;
            }

            this.conceptView.SetRelatedConcepts(this.conceptService.FindRelatedConceptsByCommitIdAndConceptId(this.commitId, this.conceptId));
        }

        public virtual void Save()
        {
            this.ActionResult = MVP.ActionResult.OK;

            this.conceptView.CloseView();
        }

        public virtual void Cancel()
        {
            this.ActionResult = MVP.ActionResult.Cancel;
            this.conceptView.CloseView();
        }

        public void AddRelatedConcept()
        {
            var conceptListPresenter = MVPSetting.PresenterFactory.GetConceptListPresenter(this.commitId);
            conceptListPresenter.Show();

            if (conceptListPresenter.ActionResult == MVP.ActionResult.OK)
            {
                foreach (var relatedConceptId in conceptListPresenter.ConceptIds)
                {
                    this.conceptService.LocalDeltas.Add(new Delta()
                    {
                        Action = DeltaAction.AddRelatedConcept,
                        ObjectId = this.conceptId,
                        Value = relatedConceptId
                    });

                    this.conceptService.LocalDeltas.Add(new Delta()
                    {
                        Action = DeltaAction.AddRelatedConcept,
                        ObjectId = relatedConceptId,
                        Value = this.conceptId
                    });
                }
            }

            this.RefreshData();
        }

        public void RemoveRelatedConcept()
        {
            var concept = this.conceptView.GetSelected();
        }
    }
}
