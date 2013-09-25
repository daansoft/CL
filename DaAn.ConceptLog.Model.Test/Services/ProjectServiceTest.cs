using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DaAn.ConceptLog.Model.Entities;
using System.Collections.Generic;

namespace DaAn.ConceptLog.Model.Test.Services
{
    [TestClass]
    public class ProjectServiceTest
    {
        [TestMethod]
        public void TestCommit()
        {
            var service = ObjectFactory.Instance.GetProjectService();

            service.Commit("test", Guid.NewGuid(), "Testtttt", "1a5a2297-63ed-4d51-90fa-09236f62ca36",
                new List<Concept>(),
                new List<Concept>() { 
                    new Concept() { 
                        Id = new Guid("6648eccc-e586-4397-8ea2-7260f6b05198"),
                        Description = "test"
                    } 
                },
                new List<Concept>());
        }
    }
}
