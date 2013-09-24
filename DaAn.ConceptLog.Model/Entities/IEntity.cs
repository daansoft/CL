using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public interface IEntity<IDT>
    {
        IDT Id { get; set; }
    }
}
