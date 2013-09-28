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
    public class EditConceptPresenter
    {
        private PresenterMode presenterMode;

        private IConceptService conceptService;
        private DeltaService deltaService;

        private IConceptView conceptView;

        private string commitId;
        private List<Delta> localDeltas;

        private Concept concept;

        public EditConceptPresenter(IConceptView conceptView, PresenterMode presenterMode, ConceptService conceptService, string conceptId, string commitId, DeltaService deltaService)
        {
            //conceptView.ConceptPresenter = this;
            this.conceptView = conceptView;

            this.presenterMode = presenterMode;

            this.conceptService = conceptService;

            this.localDeltas = new List<Delta>();
            this.commitId = commitId;
            this.deltaService = deltaService;

            Initailize(conceptId);
        }

        private void Initailize(string conceptId)
        {
            if (this.presenterMode == PresenterMode.Edit)
            {
                this.concept = this.conceptService.ReadConceptByCommitIdAndConceptId(this.commitId, conceptId);
                this.conceptView.Description = this.concept.Description;
            }
            else if (this.presenterMode == PresenterMode.Create)
            {
                this.concept = new Concept()
                {
                    Id = ProjectTools.NewId()
                };

                this.localDeltas.Add(new Delta()
                {
                    Action = DeltaAction.AddConcept,
                    ObjectId = this.concept.Id,
                    Value = this.concept.Id
                });
            }
        }

        public int Action { get; set; }

        public void Show()
        {
            this.conceptView.ShowView();
        }

        private void RefreshData()
        {
            var relatedConcepts = new List<Concept>();

            if (this.presenterMode == PresenterMode.Edit)
            {
                relatedConcepts = this.conceptService.FindRelatedConceptsByCommitIdAndConceptId(this.commitId, this.concept.Id);
            }

            this.conceptView.SetRelatedConcepts(relatedConcepts);
        }

        public void Save()
        {
            this.Action = 0;

            this.deltaService.Create(this.localDeltas);

            if (presenterMode == PresenterMode.Edit)
            {
                this.deltaService.Create(new Delta()
                {
                    Action = DeltaAction.UpdateConceptDescription,
                    ObjectId = this.concept.Id,
                    Value = this.conceptView.Description
                });
            }

            this.conceptView.CloseView();
        }

        public void Cancel()
        {
            this.Action = 1;
            this.conceptView.CloseView();
        }

        public void AddRelatedConcept()
        {
            this.localDeltas.Add(new Delta()
            {
                Action = DeltaAction.AddRelatedConcept,
                ObjectId = this.concept.Id,
                Value = "0d87f225-cb64-406c-881a-52193a602a76"
            });

            this.localDeltas.Add(new Delta()
            {
                Action = DeltaAction.AddRelatedConcept,
                ObjectId = "0d87f225-cb64-406c-881a-52193a602a76",
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
