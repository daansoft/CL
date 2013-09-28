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
    public class CreateConceptPresenter
    {
        private ConceptServiceWithGlobalAndLocalDeltas conceptService;
        private DeltaService deltaService;

        private IConceptView conceptView;

        private string commitId;

        private Concept concept;

        public CreateConceptPresenter(IConceptView conceptView, string commitId)
        {
            conceptView.ConceptPresenter = this;
            this.conceptView = conceptView;

            this.conceptService = ObjectFactory.Instance.GetConceptServiceWithGlobalAndLocalDeltas();
            this.deltaService = ObjectFactory.Instance.GetDeltaService();

            this.commitId = commitId;

            this.concept = new Concept()
            {
                Id = ProjectTools.NewId()
            };
        }

        public int Action { get; set; }

        public void Show()
        {
            this.conceptView.ShowView();
        }

        private void RefreshData()
        {
            this.conceptView.SetRelatedConcepts(this.conceptService.FindRelatedConceptsByCommitIdAndConceptId(this.commitId, this.concept.Id));
        }

        public void Save()
        {
            this.Action = 0;

            this.concept.Description = this.conceptView.Description;

            this.deltaService.Create(this.conceptService.LocalDeltas);
            this.deltaService.Create(DeltaFactory.Instance.AddConcept(this.concept.Id));
            this.deltaService.Create(DeltaFactory.Instance.UpdateConceptDescription(this.concept.Id, this.conceptView.Description));

            this.conceptView.CloseView();
        }

        public void Cancel()
        {
            this.Action = 1;
            this.conceptView.CloseView();
        }

        public void AddRelatedConcept()
        {

            //if(this.deltaService.Exists(


            this.conceptService.LocalDeltas.Add(new Delta()
            {
                Action = DeltaAction.AddRelatedConcept,
                ObjectId = this.concept.Id,
                Value = "0a09829d-edbc-4d6c-b4cb-55662769536b"
            });

            this.conceptService.LocalDeltas.Add(new Delta()
            {
                Action = DeltaAction.AddRelatedConcept,
                ObjectId = "0a09829d-edbc-4d6c-b4cb-55662769536b",
                Value = this.concept.Id
            });

            this.RefreshData();
        }

        public void RemoveRelatedConcept()
        {
            var concept = this.conceptView.GetSelected();
        }
    }
}
