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

        public void Save(List<User> users)
        {
            if (users != null)
            {
                //TODO combine
                File.WriteAllText(ProjectSettings.Path + UserFile, JsonConvert.SerializeObject(users));
            }
        }

        public List<User> Read()
        {
            var users = File.ReadAllText(ProjectSettings.Path + UserFile);
            if (!string.IsNullOrWhiteSpace(users))
            {
                return JsonConvert.DeserializeObject<List<User>>(users);
            }

            return new List<User>();
        }
    }
}
