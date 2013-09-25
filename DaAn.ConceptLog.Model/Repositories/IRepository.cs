using DaAn.ConceptLog.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Repositories
{
    public interface IRepository<ET, IDT> where ET : IEntity<IDT>
    {
    }
}
