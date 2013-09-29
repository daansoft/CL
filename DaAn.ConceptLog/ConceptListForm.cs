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
    public partial class ConceptListForm : Form, IConceptListView
    {
        public ConceptListForm()
        {
            InitializeComponent();
        }

        public ConceptListPresenter ConceptListPresenter { get; set; }

        public void SetConcepts(List<Model.Entities.Concept> concepts)
        {
            this.conceptsBS.DataSource = concepts;
            this.conceptsBS.ResetBindings(true);
        }

        public void ShowView()
        {
            this.ShowDialog();
        }

        public void SendMessage(string message)
        {
            MessageBox.Show(message);
        }

        public List<string> GetSelected()
        {
            if (this.conceptsBS.Current == null)
            {
                return null; //TODO return newList<string>
            }

            return new List<string>() { ((Concept)this.conceptsBS.Current).Id };
        }

        private void selectBT_Click(object sender, EventArgs e)
        {
            this.ConceptListPresenter.Select();
        }


        public void CloseView()
        {
            this.Close();
        }
    }
}
