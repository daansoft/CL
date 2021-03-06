﻿using DaAn.ConceptLog.Model;
using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Services;
using DaAn.ConceptLog.Model.Utils;
using DaAn.ConceptLog.MVP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP.Presenters
{
    public class EditConceptPresenter : BaseConceptPresenter
    {
        public EditConceptPresenter(IConceptView conceptView, string commitId, string conceptId) :
            base(conceptView, commitId, conceptId)
        {
        }

        public override void Save()
        {
            this.ActionResult = MVP.ActionResult.OK;

            this.deltaService.Create(this.conceptService.LocalDeltas);
            this.deltaService.Create(DeltaFactory.Instance.UpdateConceptDescription(this.conceptId, this.conceptView.Description));

            this.conceptView.CloseView();
        }
    }
}
