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
    public partial class ConceptForm : Form, IConceptView
    {
        public ConceptForm()
        {
            InitializeComponent();
        }

        public BaseConceptPresenter ConceptPresenter { get; set; }

        private void saveBT_Click(object sender, EventArgs e)
        {
            this.ConceptPresenter.Save();
        }

        public void ShowView()
        {
            this.ShowDialog();
        }

        private void cancelBT_Click(object sender, EventArgs e)
        {
            this.ConceptPresenter.Cancel();
        }

        private void addRelatedConceptBT_Click(object sender, EventArgs e)
        {
            this.ConceptPresenter.AddRelatedConcept();
        }

        private void removeRelatedConceptBT_Click(object sender, EventArgs e)
        {
            this.ConceptPresenter.RemoveRelatedConcept();
        }


        public Concept GetSelected()
        {
            throw new NotImplementedException();
        }

        public void CloseView()
        {
            this.Close();
        }


        public string Description
        {
            get
            {
                return this.descriptionTB.Text;
            }
            set
            {
                this.descriptionTB.Text = value;
            }
        }


        public void SetRelatedConcepts(List<Concept> relatedConcepts)
        {
            this.relatedConceptsBS.DataSource = relatedConcepts;
            this.relatedConceptsBS.ResetBindings(true);
        }
    }
}
