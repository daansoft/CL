using DaAn.ConceptLog.Model;
using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Services;
using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP.Presenters
{
    public class MainPresenter
    {
        private CommitService commitService;
        private ProjectDetailsService projectDetailsService;

        private IMainView mainView;
        private string path;

        private Guid usreId;

        private ProjectDetails projectDetails;

        private List<Concept> addedConcepts;
        private List<Concept> editedConcepts;
        private List<Concept> deletedConcepts;

        public MainPresenter(IMainView mainView)
        {
            mainView.MainPresenter = this;

            this.mainView = mainView;

            this.addedConcepts = new List<Concept>();
            this.editedConcepts = new List<Concept>();
            this.deletedConcepts = new List<Concept>();

            this.commitService = ObjectFactory.Instance.GetCommitService();
            this.projectDetailsService = ObjectFactory.Instance.GetProjectDetailsService();
        }

        public void OpenProject(string path)
        {
            this.projectDetails = projectDetailsService.Read(path);

            this.path = path;

            this.mainView.SetConcepts(new List<Concept>());
        }

        public void SaveProject()
        {
            projectDetailsService.Save(this.path, this.projectDetails);
        }

        public void NewProject(string path, string name, string description)
        {
            if (projectDetailsService.Exists(path))
            {
                this.mainView.SendMessage("Projekt istnieje");
                return;
            }

            this.path = path;

            this.projectDetails = new ProjectDetails()
            {
                Name = name,
                Description = description,
                BranchName = "master",
                CommitId = null
            };

            projectDetailsService.Save(path, this.projectDetails);


        }

        public void Commit()
        {
            var description = "Test";
            this.projectDetails.CommitId = this.commitService.Commit(this.path, this.usreId, description, this.projectDetails.CommitId, this.addedConcepts, this.editedConcepts, this.deletedConcepts);
        }
    }
}
