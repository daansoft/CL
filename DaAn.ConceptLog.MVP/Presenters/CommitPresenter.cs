﻿using DaAn.ConceptLog.Model;
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
        private IConceptService conceptService;
        private CommitService commitService;
        private ProjectDetailsService projectDetailsService;

        private ICommitView commitView;

        private Guid userId;

        private ProjectDetails projectDetails;

        public CommitPresenter(ICommitView mainView, Guid userId, ProjectDetails projectDetails)
        {
            mainView.CommitPresenter = this;

            this.commitView = mainView;

            this.userId = userId;
            this.projectDetails = projectDetails;

            this.conceptService = ObjectFactory.Instance.GetConceptServiceWithDeltas();
            this.projectDetailsService = ObjectFactory.Instance.GetProjectDetailsService();
            this.commitService = ObjectFactory.Instance.GetCommitService();
        }

        public void Commit()
        {
            var description = this.commitView.GetCommitMessage();
            var concepts = this.conceptService.FindByBranchName(this.projectDetails.BranchName);

            this.commitService.Commit(this.userId, description, this.projectDetails, concepts);

            this.commitView.CloseView();

        }

        public void Show()
        {
            this.commitView.ShowView();
        }
    }
}
