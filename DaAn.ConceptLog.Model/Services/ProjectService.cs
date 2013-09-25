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
        private BranchRepository branchRepository;
        private CommitRepository commitRepository;
        private UserRepository userRepository;

        public ProjectService(ProjectDetailsRepository projectDetailsRepository,
            BranchRepository branchRepository,
            CommitRepository commitRepository,
            UserRepository userRepository)
        {
            this.projectDetailsRepository = projectDetailsRepository;
            this.branchRepository = branchRepository;
            this.commitRepository = commitRepository;
            this.userRepository = userRepository;
        }

        public void Write(Project project)
        {
            this.projectDetailsRepository.Save(project.Path, project.Details);
            this.branchRepository.Save(project.Path, project.Braches);
            this.commitRepository.Save(project.Path, project.Commits);
            this.userRepository.Save(project.Path, project.Users);
        }

        public Project Read(string path)
        {
            var project = new Project();
            project.Path = path;
            project.Details = this.projectDetailsRepository.Read(path);
            project.Braches = this.branchRepository.Read(path);
            project.Commits = this.commitRepository.FindAll(path);
            project.Users = this.userRepository.Read(path);

            return project;
        }
    }
}
