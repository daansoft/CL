using DaAn.ConceptLog.Model.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Services
{
    public class ProjectService
    {
        public Project Read(string path)
        {
            var project = new Project();

            var details = File.ReadAllText(path + "Details.clpd");
            if (!string.IsNullOrWhiteSpace(details))
            {
                project.Details = JsonConvert.DeserializeObject<ProjectDetails>(details);
            }

            var branches = File.ReadAllText(path + "Branches.clb");
            if (!string.IsNullOrWhiteSpace(branches))
            {
                project.Braches = JsonConvert.DeserializeObject<List<Branch>>(branches);
            }

            var commits = File.ReadAllText(path + "Commits.clc");
            if (!string.IsNullOrWhiteSpace(commits))
            {
                project.Commits = JsonConvert.DeserializeObject<List<Commit>>(commits);
            }


            return project;
        }
    }
}
