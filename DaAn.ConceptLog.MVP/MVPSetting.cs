using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.MVP
{
    public class MVPSetting
    {
        public static PresenterFactory PresenterFactory;

        static MVPSetting()
        {
            MVPSetting.PresenterFactory = new PresenterFactory();
        }
    }
}
