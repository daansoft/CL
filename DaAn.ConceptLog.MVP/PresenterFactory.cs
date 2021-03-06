﻿using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Services;
using DaAn.ConceptLog.MVP.Presenters;
using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP
{
    public class PresenterFactory
    {
        public IViewFactory ViewFactory { get; set; }

        public MainPresenter GetMainPresenter()
        {
            return new MainPresenter(this.ViewFactory.GetMainView());
        }

        public CommitPresenter GetCommitPresenter(Guid userId, ProjectDetails projectDetails)
        {
            return new CommitPresenter(this.ViewFactory.GetCommitView(), userId, projectDetails);
        }

        public CreateConceptPresenter GetCreateConceptPresenter(string commitId)
        {
            return new CreateConceptPresenter(this.ViewFactory.GetConceptView(), commitId);
        }

        public EditConceptPresenter GetEditConceptPresenter(string commitId, string conceptId)
        {
            return new EditConceptPresenter(this.ViewFactory.GetConceptView(), commitId, conceptId);
        }

        public ConceptListPresenter GetConceptListPresenter(string commitId)
        {
            return new ConceptListPresenter(this.ViewFactory.GetConceptListView(), commitId);
        }
    }
}
