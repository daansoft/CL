using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP
{
    public interface IViewFactory
    {
        IMainView GetMainView();
        ICommitView GetCommitView();
        IConceptView GetConceptView();
        IConceptListView GetConceptListView();
    }
}
