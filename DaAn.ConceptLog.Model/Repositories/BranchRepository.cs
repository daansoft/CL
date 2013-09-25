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
        public void Save(string path, Branch branch)
        {
            var directory = Path.Combine(path, "branches");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(Path.Combine(directory, branch.Name), JsonConvert.SerializeObject(branch));
        }

        public void Save(string path, List<Branch> branches)
        {
            foreach (var branch in branches)
            {
                this.Save(path, branch);
            }
        }

        public Branch Read(string path, string name)
        {
            var file = Path.Combine(path, "branches", name);

            if (!File.Exists(file))
            {
                return null; // TODO throw exception
            }

            return JsonConvert.DeserializeObject<Branch>(File.ReadAllText(file));
        }

        public List<Branch> FindAll(string path)
        {
            var result = new List<Branch>();
            var directory = Path.Combine(path, "branches");

            if (!Directory.Exists(directory))
            {
                return result;
            }

            foreach (var branchFile in Directory.GetFiles(directory))
            {
                result.Add(this.Read(path, Path.GetFileName(branchFile)));
            }

            return result;
        }
    }
}
