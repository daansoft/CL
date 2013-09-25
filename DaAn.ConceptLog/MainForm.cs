using DaAn.ConceptLog.Model.Entities;
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
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public MainPresenter MainPresenter { get; set; }

        public void SetConcepts(List<Concept> concepts)
        {
            throw new NotImplementedException();
        }
    }
}
