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
        private ProjectService projectService;
        private ProjectDetailsService projectDetailsService;

        private ICommitView commitView;
        private string path;

        private Guid usreId;

        private ProjectDetails projectDetails;

        private List<Concept> addedConcepts;
        private List<Concept> editedConcepts;
        private List<Concept> deletedConcepts;

        public CommitPresenter(ICommitView mainView)
        {
            mainView.CommitPresenter = this;

            this.commitView = mainView;

            this.addedConcepts = new List<Concept>();
            this.editedConcepts = new List<Concept>();
            this.deletedConcepts = new List<Concept>();

            this.projectService = ObjectFactory.Instance.GetProjectService();
            this.projectDetailsService = ObjectFactory.Instance.GetProjectDetailsService();
        }

        public void Commit()
        {
            var description = "Test";
            this.projectDetails.CommitId = this.projectService.Commit(this.path, this.usreId, description, this.projectDetails.CommitId, this.addedConcepts, this.editedConcepts, this.deletedConcepts);
        }

        public void Show()
        {
            this.commitView.Show();
        }
    }
}
