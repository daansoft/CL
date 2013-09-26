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

        private ConceptService conceptService;
        private ProjectDetailsService projectDetailsService;
        private BranchService branchService;

        private IMainView mainView;
        private string path;

        private Guid userId;

        private ProjectDetails projectDetails;

        private List<Concept> addedConcepts;
        private List<Concept> editedConcepts;
        private List<Concept> deletedConcepts;

        public MainPresenter(IMainView mainView)
        {
            mainView.MainPresenter = this;

            this.mainView = mainView;
            this.userId = Guid.NewGuid();

            this.addedConcepts = new List<Concept>();
            this.editedConcepts = new List<Concept>();
            this.deletedConcepts = new List<Concept>();

            this.conceptService = ObjectFactory.Instance.GetConceptService();
            this.projectDetailsService = ObjectFactory.Instance.GetProjectDetailsService();
            this.branchService = ObjectFactory.Instance.GetBranchService();
        }

        public void OpenProject(string path)
        {
            if (!this.projectDetailsService.Exists(path))
            {
                this.mainView.SendMessage("Projekt nie istnieje");
                return;
            }

            this.projectDetails = this.projectDetailsService.Read(path);

            this.path = path;
            this.RefreshData();
        }

        private void RefreshData()
        {
            var concepts = new List<Concept>();

            concepts.AddRange(this.conceptService.FindByBranchName(this.path, this.projectDetails.BranchName));

            this.mainView.SetConcepts(this.conceptService.FindByBranchName(this.path, this.projectDetails.BranchName));
        }

        public void SaveProject()
        {
            this.projectDetailsService.Save(this.path, this.projectDetails);
        }

        public void NewProject(string path, string name, string description)
        {
            if (this.projectDetailsService.Exists(path))
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
                PreviuosCommitId = null
            };

            this.projectDetailsService.Save(path, this.projectDetails);
            this.branchService.Save(path, new Branch()
            {
                CommitId = null,
                Name = this.projectDetails.BranchName
            });

        }

        public void Commit()
        {
            var commitPresenter = MVPSetting.Factory.GetCommitPresenter(this.path,
                this.userId,
                this.projectDetails,
                this.addedConcepts,
                this.editedConcepts,
                this.deletedConcepts);

            commitPresenter.Show();
        }

        public void AddNewConcept()
        {
            this.addedConcepts.Add(new Concept()
            {
                CreatorId = this.userId,
                Description = "Opis",
                Id = Guid.NewGuid()
            });

        }
    }
}
