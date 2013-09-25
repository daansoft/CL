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
    public class BlobRepository
    {
        public void Save(string path, Blob blob)
        {
            var directory = Path.Combine(path, "blobs");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(Path.Combine(directory, blob.Id), JsonConvert.SerializeObject(blob));
        }

        public void Save(string path, List<Blob> blobs)
        {
            foreach (var blob in blobs)
            {
                this.Save(path, blob);
            }
        }

        public Blob Read(string path, string id)
        {
            if (id == null)
            {
                return null;
            }

            var file = Path.Combine(path, "blobs", id);

            if (!File.Exists(file))
            {
                return null; // TODO throw exception
            }

            return JsonConvert.DeserializeObject<Blob>(File.ReadAllText(file));
        }

        public List<Blob> FindAll(string path)
        {
            var result = new List<Blob>();
            var directory = Path.Combine(path, "blobs");

            if (!Directory.Exists(directory))
            {
                return result;
            }

            foreach (var blobFile in Directory.GetFiles(directory))
            {
                result.Add(this.Read(path, Path.GetFileName(blobFile)));
            }

            return result;
        }
    }
}
