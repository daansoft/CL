using DaAn.ConceptLog.MVP;
using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog
{
    public class WinFormsMVPFactory : MVPFactory
    {

        public override IMainView GetMainView()
        {
            return new MainForm();
        }

        public override ICommitView GetCommitView()
        {
            return new CommitForm();
        }
    }
}
