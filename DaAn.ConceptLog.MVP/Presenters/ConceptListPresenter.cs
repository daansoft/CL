using DaAn.ConceptLog.Model.Entities;
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
        private IConceptListView conceptListView;

        private Commit commit;

        public ConceptListPresenter(IConceptListView conceptListView, Commit commit)
        {
            conceptListView.ConceptListPresenter = this;
            this.conceptListView = conceptListView;

            this.commit = commit;
        }

        void Show()
        {


            this.conceptListView.Show();
        }
    }
}
