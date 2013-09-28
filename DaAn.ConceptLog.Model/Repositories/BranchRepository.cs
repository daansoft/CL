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
    public class BranchRepository
    {
        public void Save(Branch branch)
        {
            var directory = Path.Combine(ProjectSettings.Path, "branches");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(Path.Combine(directory, branch.Name), JsonConvert.SerializeObject(branch));
        }

        public void Save(List<Branch> branches)
        {
            foreach (var branch in branches)
            {
                this.Save(branch);
            }
        }

        public Branch Read(string name)
        {
            var file = Path.Combine(ProjectSettings.Path, "branches", name);

            if (!File.Exists(file))
            {
                return null; // TODO throw exception
            }

            return JsonConvert.DeserializeObject<Branch>(File.ReadAllText(file));
        }

        public List<Branch> FindAll()
        {
            var result = new List<Branch>();
            var directory = Path.Combine(ProjectSettings.Path, "branches");

            if (!Directory.Exists(directory))
            {
                return result;
            }

            foreach (var branchFile in Directory.GetFiles(directory))
            {
                result.Add(this.Read(Path.GetFileName(branchFile)));
            }

            return result;
        }
    }
}
