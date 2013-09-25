using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Services
{
    public interface IService<RT>
    {
        RT Repository { get; set; }
    }
}
