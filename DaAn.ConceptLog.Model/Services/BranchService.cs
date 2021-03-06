﻿using DaAn.ConceptLog.Model.Entities;
using DaAn.ConceptLog.Model.Repositories;
using DaAn.ConceptLog.Model.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model.Services
{
    public class BranchService
    {
        private BranchRepository branchRepository;

        public BranchService(BranchRepository branchRepository)
        {
            this.branchRepository = branchRepository;
        }

        public Branch Read(string name)
        {
            return this.branchRepository.Read(name);
        }

        public void Save(Branch branch)
        {
            this.branchRepository.Save(branch);
        }
    }
}
