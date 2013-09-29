using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.MVP.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP.Views
{
    public interface IConceptListView
    {
        ConceptListPresenter ConceptListPresenter { get; set; }
        
        void SetConcepts(List<Concept> concepts);
        void ShowView();
        void SendMessage(string message);

        List<string> GetSelected();

        void CloseView();
    }
}
