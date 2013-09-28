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
        public void Save(Commit commit)
        {
            var directory = Path.Combine(ProjectSettings.Path, "commits");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(Path.Combine(directory, commit.Id), JsonConvert.SerializeObject(commit));
        }

        public void Save(List<Commit> commits)
        {
            foreach (var commit in commits)
            {
                this.Save(commit);
            }
        }

        public Commit Read(string id)
        {
            if (id == null)
            {
                return null;
            }

            var file = Path.Combine(ProjectSettings.Path, "commits", id);

            if (!File.Exists(file))
            {
                return null; // TODO throw exception
            }

            return JsonConvert.DeserializeObject<Commit>(File.ReadAllText(file));
        }

        public List<Commit> FindAll()
        {
            var result = new List<Commit>();
            var directory = Path.Combine(ProjectSettings.Path, "commits");

            if (!Directory.Exists(directory))
            {
                return result;
            }

            foreach (var commitFile in Directory.GetFiles(directory))
            {
                result.Add(this.Read(Path.GetFileName(commitFile)));
            }

            return result;
        }
    }
}
