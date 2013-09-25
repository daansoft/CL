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
    public class ProjectDetailsRepository
    {
        public void Save(string path, ProjectDetails details)
        {
            if (details != null)
            {
                File.WriteAllText(path + "Details.clpd", JsonConvert.SerializeObject(details));
            }
        }

        public ProjectDetails Read(string path)
        {
            var details = File.ReadAllText(path + "Details.clpd");
            if (!string.IsNullOrWhiteSpace(details))
            {
                return JsonConvert.DeserializeObject<ProjectDetails>(details);
            }

            return new ProjectDetails();
        }
    }
}
