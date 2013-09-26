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

        void ShowView();
    }
}
