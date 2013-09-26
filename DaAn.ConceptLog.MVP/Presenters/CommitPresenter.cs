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

        private List<Concept> addedConcepts;
        private List<Concept> editedConcepts;
        private List<Concept> deletedConcepts;

        public CommitPresenter(ICommitView mainView, string path, Guid userId, ProjectDetails projectDetails, List<Concept> addedConcepts, List<Concept> editedConcepts, List<Concept> deletedConcepts)
        {
            mainView.CommitPresenter = this;

            this.commitView = mainView;

            this.path = path;
            this.userId = userId;
            this.projectDetails = projectDetails;

            this.addedConcepts = addedConcepts;
            this.editedConcepts = editedConcepts;
            this.deletedConcepts = deletedConcepts;

            this.conceptService = ObjectFactory.Instance.GetConceptService();
            this.projectDetailsService = ObjectFactory.Instance.GetProjectDetailsService();
        }

        public void Commit()
        {
            var description = this.commitView.GetCommitMessage();
            this.conceptService.Commit(this.path, this.userId, description, this.projectDetails, this.addedConcepts, this.editedConcepts, this.deletedConcepts);

        }

        public void Show()
        {
            this.commitView.Show();
        }
    }
}
