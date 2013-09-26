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
            this.conceptsBS.DataSource = concepts;
            this.conceptsBS.ResetBindings(true);
        }

        private void openProjectMI_Click(object sender, EventArgs e)
        {
            var path = "Test";
            this.MainPresenter.OpenProject(path);
        }

        private void saveProjectMI_Click(object sender, EventArgs e)
        {
            this.MainPresenter.SaveProject();
        }

        private void newProjectMI_Click(object sender, EventArgs e)
        {
            this.MainPresenter.NewProject("Test", "Project", "Project");
        }


        public void SendMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void commitMI_Click(object sender, EventArgs e)
        {
            this.MainPresenter.Commit();
        }

        private void addNewConceptMI_Click(object sender, EventArgs e)
        {
            this.MainPresenter.AddNewConcept();
        }
    }
}
