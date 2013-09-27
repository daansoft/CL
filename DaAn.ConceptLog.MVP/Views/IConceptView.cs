using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.MVP.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP.Views
{
    public interface IConceptView
    {
        CreateConceptPresenter ConceptPresenter { get; set; }

        void ShowView();

        void CloseView();

        Concept GetSelected();

        string Description { get; set; }

        void SetRelatedConcepts(List<Concept> relatedConcepts);
    }
}
