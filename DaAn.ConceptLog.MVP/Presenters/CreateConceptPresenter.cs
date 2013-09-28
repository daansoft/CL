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
        private ConceptServiceLocalDeltaDecorator conceptService;
        private DeltaService deltaService;

        private IConceptView conceptView;

        private string path;
        private string commitId;

        private Concept concept;

        public CreateConceptPresenter(IConceptView conceptView, string path, string commitId)
        {
            conceptView.ConceptPresenter = this;
            this.conceptView = conceptView;

            this.conceptService = ObjectFactory.Instance.GetConceptServiceWithLocalDeltas();
            this.deltaService = ObjectFactory.Instance.GetDeltaService();

            this.path = path;
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
            this.conceptView.SetRelatedConcepts(this.conceptService.FindRelatedConceptsByCommitIdAndConceptId(this.path, this.commitId, this.concept.Id));
        }

        public void Save()
        {
            this.Action = 0;

            this.concept.Description = this.conceptView.Description;

            this.deltaService.Create(this.conceptService.LocalDeltas);
            this.deltaService.Create(new Delta()
            {
                Action = DeltaAction.AddConcept,
                ObjectId = this.concept.Id,
                Value = this.concept
            });

            this.conceptView.CloseView();
        }

        public void Cancel()
        {
            this.Action = 1;
            this.conceptView.CloseView();
        }

        public void AddRelatedConcept()
        {
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
