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
        private static readonly string ProjectDetailsFile = "project.clpr";

        public void Save(ProjectDetails details)
        {
            if (!Directory.Exists(ProjectSettings.Path))
            {
                Directory.CreateDirectory(ProjectSettings.Path);
            }

            File.WriteAllText(Path.Combine(ProjectSettings.Path, ProjectDetailsFile), JsonConvert.SerializeObject(details));
        }

        public ProjectDetails Read()
        {
            return JsonConvert.DeserializeObject<ProjectDetails>(File.ReadAllText(Path.Combine(ProjectSettings.Path, ProjectDetailsFile)));
        }

        public bool Exists()
        {
            return File.Exists(Path.Combine(ProjectSettings.Path, ProjectDetailsFile));
        }
    }
}
