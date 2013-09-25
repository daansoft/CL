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
    public class CommitRepository
    {
        public void Save(string path, Commit commit)
        {
            var directory = Path.Combine(path, "commits");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(Path.Combine(directory, commit.Id), JsonConvert.SerializeObject(commit));
        }

        public void Save(string path, List<Commit> commits)
        {
            foreach (var commit in commits)
            {
                this.Save(path, commit);
            }
        }

        public Commit Read(string path, string id)
        {
            if (id == null)
            {
                return null;
            }

            var file = Path.Combine(path, "commits", id);

            if (!File.Exists(file))
            {
                return null; // TODO throw exception
            }

            return JsonConvert.DeserializeObject<Commit>(File.ReadAllText(file));
        }

        public List<Commit> FindAll(string path)
        {
            var result = new List<Commit>();
            var directory = Path.Combine(path, "commits");

            if (!Directory.Exists(directory))
            {
                return result;
            }

            foreach (var commitFile in Directory.GetFiles(directory))
            {
                result.Add(this.Read(path, Path.GetFileName(commitFile)));
            }

            return result;
        }
    }
}
