using DaAn.ConceptLog.Model;
using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Services;
using DaAn.ConceptLog.Model.Utils;
using DaAn.ConceptLog.MVP.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP.Presenters
{
    public class MainPresenter
    {
        private IConceptService conceptService;
        private ProjectDetailsService projectDetailsService;
        private BranchService branchService;
        private DeltaService deltaService;

        private IMainView mainView;

        private Guid userId;
        private ProjectDetails projectDetails;

        public MainPresenter(IMainView mainView)
        {
            mainView.MainPresenter = this;

            this.mainView = mainView;
            this.userId = Guid.NewGuid();

            this.conceptService = ObjectFactory.Instance.GetConceptServiceWithDeltas();
            this.projectDetailsService = ObjectFactory.Instance.GetProjectDetailsService();
            this.branchService = ObjectFactory.Instance.GetBranchService();
            this.deltaService = ObjectFactory.Instance.GetDeltaService();
        }

        public void OpenProject(string path)
        {
            ProjectSettings.Path = path;
            if (!this.projectDetailsService.Exists())
            {
                this.mainView.SendMessage("Projekt nie istnieje");
                return;
            }


            this.projectDetails = this.projectDetailsService.Read();

            this.deltaService.DeleteAll();

            ProjectSettings.Path = path;
            this.RefreshData();
        }

        private void RefreshData()
        {
            var concepts = this.conceptService.FindByCommitId(this.projectDetails.PreviuosCommitId);

            this.mainView.SetConcepts(concepts);
        }

        public void SaveProject()
        {
            this.projectDetailsService.Save(this.projectDetails);
        }

        public void NewProject(string name, string description)
        {
            ProjectSettings.Path = "test";
            if (this.projectDetailsService.Exists())
            {
                this.mainView.SendMessage("Projekt istnieje");
                ProjectSettings.Path = null;
                return;
            }

            this.projectDetails = new ProjectDetails()
            {
                Name = name,
                Description = description,
                BranchName = "master",
                PreviuosCommitId = null
            };

            this.projectDetailsService.Save(this.projectDetails);
            this.branchService.Save(new Branch()
            {
                CommitId = null,
                Name = this.projectDetails.BranchName
            });

        }

        public void Commit()
        {
            var commitPresenter = MVPSetting.PresenterFactory.GetCommitPresenter(this.userId, this.projectDetails);

            commitPresenter.Show();

            this.RefreshData();
        }

        public void AddNewConcept()
        {
            var conceptPresenter = MVPSetting.PresenterFactory.GetCreateConceptPresenter(this.projectDetails.PreviuosCommitId);
            conceptPresenter.Show();

            this.RefreshData();
        }

        public void Details()
        {
            var concept = this.mainView.GetSelected();

            this.mainView.SendMessage(JsonConvert.SerializeObject(concept));
        }

        public void EditConcept()
        {
            var concept = this.mainView.GetSelected();

            var conceptPresenter = MVPSetting.PresenterFactory.GetEditConceptPresenter(this.projectDetails.PreviuosCommitId, concept.Id);
            conceptPresenter.Show();

            this.RefreshData();
        }
    }
}
