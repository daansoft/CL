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
    public class ProjectDetailsService
    {
        public ProjectDetailsRepository projectDetailsRepository { get; set; }

        public ProjectDetailsService(ProjectDetailsRepository projectDetailsRepository)
        {
            this.projectDetailsRepository = projectDetailsRepository;
        }

        public ProjectDetails Read(string path)
        {
            return this.projectDetailsRepository.Read(path);
        }

        public void Save(string path, ProjectDetails details)
        {
            this.projectDetailsRepository.Save(path, details);
        }

        public bool Exists(string path)
        {
            return this.projectDetailsRepository.Exists(path);
        }
    }
}
