using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Entities
{
    public class User
    {
        Guid Id { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string EMail { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Telephone { get; set; }
        string Info { get; set; }
        byte[] Image { get; set; }
    }
}
