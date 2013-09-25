using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Repositories;
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
        private ProjectDetailsRepository projectDetailsRepository;

        public void Write(Project project)
        {
            this.projectDetailsRepository.Save(project.Path, project.Details);

            if (project.Braches != null)
            {
                File.WriteAllText(project.Path + "Branches.clb", JsonConvert.SerializeObject(project.Braches));
            }

            if (project.Commits != null)
            {
                File.WriteAllText(project.Path + "Commits.clc", JsonConvert.SerializeObject(project.Commits));
            }

            if (project.Users != null)
            {
                File.WriteAllText(project.Path + "Users.clc", JsonConvert.SerializeObject(project.Users));
            }
        }

        public Project Read(string path)
        {
            var project = new Project();

            project.Path = path;

            project.Details = this.projectDetailsRepository.Read(path);

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

            var users = File.ReadAllText(path + "Users.clc");
            if (!string.IsNullOrWhiteSpace(commits))
            {
                project.Users = JsonConvert.DeserializeObject<List<User>>(users);
            }

            return project;
        }
    }
}
