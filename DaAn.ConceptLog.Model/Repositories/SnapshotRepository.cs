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
    public class SnapshotRepository
    {
        public void Save(string path, Snapshot snapshot)
        {
            var directory = Path.Combine(path, "snapshots");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(Path.Combine(directory, snapshot.Id), JsonConvert.SerializeObject(snapshot));
        }

        public void Save(string path, List<Snapshot> snapshots)
        {
            foreach (var snapshot in snapshots)
            {
                this.Save(path, snapshot);
            }
        }

        public Snapshot Read(string path, string id)
        {
            var file = Path.Combine(path, "snapshots", id);

            if (!File.Exists(file))
            {
                return null; // TODO throw exception
            }

            return JsonConvert.DeserializeObject<Snapshot>(File.ReadAllText(file));
        }

        public List<Snapshot> FindAll(string path)
        {
            var result = new List<Snapshot>();
            var directory = Path.Combine(path, "snapshots");

            if (!Directory.Exists(directory))
            {
                return result;
            }

            foreach (var snapshotFile in Directory.GetFiles(directory))
            {
                result.Add(this.Read(path, Path.GetFileName(snapshotFile)));
            }

            return result;
        }
    }
}
