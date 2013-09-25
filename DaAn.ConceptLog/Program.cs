using DaAn.ConceptLog.MVP;
using DaAn.ConceptLog.MVP.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaAn.ConceptLog
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MVPSetting.Factory = new WinFormsMVPFactory();

            MainForm form = new MainForm();
            MainPresenter presenter = new MainPresenter(form);
            Application.Run(form);
        }
    }
}
