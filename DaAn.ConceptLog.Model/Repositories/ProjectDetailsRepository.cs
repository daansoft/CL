﻿using DaAn.ConceptLog.Model.Entities;
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
        private static readonly string ProjectDetailsFile = "project_details.clpd";

        public void Save(string path, ProjectDetails details)
        {
            File.WriteAllText(Path.Combine(path, ProjectDetailsFile), JsonConvert.SerializeObject(details));
        }

        public ProjectDetails Read(string path)
        {
            return JsonConvert.DeserializeObject<ProjectDetails>(File.ReadAllText(Path.Combine(path, ProjectDetailsFile)));
        }

        public bool Exists(string path)
        {
            return File.Exists(Path.Combine(path, ProjectDetailsFile));
        }
    }
}