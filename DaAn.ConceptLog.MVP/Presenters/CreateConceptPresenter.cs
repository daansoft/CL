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
        private IConceptService conceptService;
        private DeltaService deltaService;

        private IConceptView conceptView;

        private string path;
        private string commitId;
        private List<Delta> localDeltas;

        private Concept concept;

        public CreateConceptPresenter(IConceptView conceptView, IConceptService conceptService, string path, string commitId, DeltaService deltaService)
        {
            conceptView.ConceptPresenter = this;
            this.conceptView = conceptView;

            this.conceptService = conceptService;

            this.localDeltas = new List<Delta>();
            this.path = path;
            this.deltaService = deltaService;
            this.commitId = commitId;
        }

        public int Action { get; set; }

        public void Show()
        {
            this.conceptView.ShowView();
        }

        private void RefreshData()
        {
            var relatedConcepts = new List<Concept>();

            relatedConcepts = this.conceptService.FindRelatedConceptsByCommitIdAndConceptId(this.path, this.commitId, this.concept.Id);

            //relatedConcepts = this.conceptService.PrepareConcepts(relatedConcepts, this.deltas.ToList()); // .Where(r => r.ObjectId == this.concept.Id)

            //relatedConcepts = this.conceptService.PrepareConcepts(relatedConcepts, this.localDeltas.ToList());

            this.conceptView.SetRelatedConcepts(relatedConcepts);
        }

        public void Save()
        {
            this.Action = 0;

            var concept = new Concept()
            {
                Id = ProjectTools.NewId(),
                Description = this.conceptView.Description
            };

            this.deltaService.Create(this.localDeltas);
            this.deltaService.Create(new Delta()
            {
                Action = DeltaAction.AddConcept,
                ObjectId = concept.Id,
                Value = concept
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
