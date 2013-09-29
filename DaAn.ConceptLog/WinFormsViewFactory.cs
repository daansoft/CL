using DaAn.ConceptLog.MVP;
using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog
{
    public class WinFormsViewFactory : IViewFactory
    {
        public IMainView GetMainView()
        {
            return new MainForm();
        }

        public ICommitView GetCommitView()
        {
            return new CommitForm();
        }


        public IConceptView GetConceptView()
        {
            return new ConceptForm();
        }


        public IConceptListView GetConceptListView()
        {
            return new ConceptListForm();
        }
    }
}
