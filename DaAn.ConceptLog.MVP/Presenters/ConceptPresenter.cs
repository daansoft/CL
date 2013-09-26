using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP.Presenters
{
    public class ConceptPresenter
    {
        private IConceptView conceptView;

        private string conceptId;

        public ConceptPresenter(IConceptView conceptView, string conceptId)
        {
            conceptView.ConceptPresenter = this;
            this.conceptView = conceptView;

            this.conceptId = conceptId;
        }

        public int Action { get; set; }

        public void Show()
        {
            if (conceptId != null)
            {

                this.conceptView.Description = this.conceptId;// TODO
            }

            this.conceptView.ShowView();
        }

        public void Save()
        {
            this.Action = 0;

            var concept = new Concept()
            {
                Id = this.conceptId
            };

            concept.Description = this.conceptView.Description;

            this.conceptView.CloseView();
        }

        public void Cancel()
        {
            this.Action = 1;
            this.conceptView.CloseView();
        }

        public void AddRelatedConcept()
        {
            throw new NotImplementedException();
        }

        public void RemoveRelatedConcept()
        {
            var concept = this.conceptView.GetSelected();
        }
    }
}
