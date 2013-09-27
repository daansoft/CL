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
    public class CommitPresenter
    {
        private ConceptService conceptService;
        private ProjectDetailsService projectDetailsService;

        private ICommitView commitView;
        private string path;

        private Guid userId;

        private ProjectDetails projectDetails;

        private List<Delta> deltas;

        public CommitPresenter(ICommitView mainView, string path, Guid userId, ProjectDetails projectDetails, List<Delta> deltas)
        {
            mainView.CommitPresenter = this;

            this.commitView = mainView;

            this.path = path;
            this.userId = userId;
            this.projectDetails = projectDetails;

            this.deltas = deltas;

            this.conceptService = ObjectFactory.Instance.GetConceptService();
            this.projectDetailsService = ObjectFactory.Instance.GetProjectDetailsService();
        }

        public void Commit()
        {
            var description = this.commitView.GetCommitMessage();
            var concepts = this.conceptService.FindByBranchName(this.path, this.projectDetails.BranchName, new List<Delta>());

            this.conceptService.Commit(this.path, this.userId, description, this.projectDetails, this.deltas);

            this.commitView.CloseView();

        }

        public void Show()
        {
            this.commitView.ShowView();
        }
    }
}
