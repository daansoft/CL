using DaAn.ConceptLog.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP.Views
{
    public interface IMainView
    {
        void SetConcepts(List<Concept> concepts);
        void Show();
    }
}
