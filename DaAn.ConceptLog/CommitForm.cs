using DaAn.ConceptLog.MVP.Presenters;
using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaAn.ConceptLog
{
    public partial class CommitForm : Form, ICommitView
    {
        public CommitForm()
        {
            InitializeComponent();
        }

        public CommitPresenter CommitPresenter { get; set; }

        public void SetConcepts(List<Model.Entities.Concept> concepts)
        {

        }

        public void SendMessage(string message)
        {
        }
    }
}
