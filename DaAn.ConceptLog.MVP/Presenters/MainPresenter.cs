using DaAn.ConceptLog.Model.Entities;
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
        private IMainView mainView;

        private Project currentProject;
        private Commit master;

        public MainPresenter(IMainView mainView)
        {
            mainView.MainPresenter = this;

            this.mainView = mainView;
        }
    }
}
