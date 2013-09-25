using DaAn.ConceptLog.Model.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Repositories
{
    public class UserRepository
    {
        private static readonly string UserFile = "users.clu";

        public void Save(string path, List<User> users)
        {
            if (users != null)
            {
                File.WriteAllText(path + UserFile, JsonConvert.SerializeObject(users));
            }
        }

        public List<User> Read(string path)
        {
            var users = File.ReadAllText(path + UserFile);
            if (!string.IsNullOrWhiteSpace(users))
            {
                return JsonConvert.DeserializeObject<List<User>>(users);
            }

            return new List<User>();
        }
    }
}
