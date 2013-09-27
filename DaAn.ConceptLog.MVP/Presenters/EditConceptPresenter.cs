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

        private ConceptService conceptService;

        private IConceptView conceptView;

        private string path;
        private string commitId;
        private List<Delta> deltas;
        private List<Delta> localDeltas;

        private Concept concept;

        public EditConceptPresenter(IConceptView conceptView, PresenterMode presenterMode, ConceptService conceptService, string conceptId, string path, string commitId, List<Delta> deltas)
        {
            //conceptView.ConceptPresenter = this;
            this.conceptView = conceptView;

            this.presenterMode = presenterMode;

            this.conceptService = conceptService;

            this.localDeltas = new List<Delta>();
            this.path = path;
            this.commitId = commitId;
            this.deltas = deltas;

            Initailize(conceptId);
        }

        private void Initailize(string conceptId)
        {
            if (this.presenterMode == PresenterMode.Edit)
            {
                this.concept = this.conceptService.ReadConceptByCommitIdAndConceptId(this.path, this.commitId, conceptId, this.deltas);
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
                    Action = DeltaAction.Add,
                    Key = DeltaKey.Concept,
                    ObjectId = this.concept.Id,
                    Value = this.concept
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
                relatedConcepts = this.conceptService.FindRelatedConceptsByCommitIdAndConceptId(this.path, this.commitId, this.concept.Id, this.deltas);
            }

            //relatedConcepts = this.conceptService.PrepareConcepts(relatedConcepts, this.deltas.ToList()); // .Where(r => r.ObjectId == this.concept.Id)

            //relatedConcepts = this.conceptService.PrepareConcepts(relatedConcepts, this.localDeltas.ToList());

            this.conceptView.SetRelatedConcepts(relatedConcepts);
        }

        public void Save()
        {
            this.Action = 0;

            this.deltas.AddRange(this.localDeltas);

            if (presenterMode == PresenterMode.Edit)
            {
                this.deltas.Add(new Delta()
                {
                    Action = DeltaAction.Update,
                    Key = DeltaKey.ConceptDescription,
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
                Action = DeltaAction.Add,
                Key = DeltaKey.RelatedConcept,
                ObjectId = this.concept.Id,
                Value = "0d87f225-cb64-406c-881a-52193a602a76"
            });

            this.localDeltas.Add(new Delta()
            {
                Action = DeltaAction.Add,
                Key = DeltaKey.RelatedConcept,
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
