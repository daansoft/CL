using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Utils
{
    public class ProjectTools
    {
        public static string NewId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
