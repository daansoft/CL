using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DaAn.ConceptLog.Model.Entities;

namespace DaAn.ConceptLog.Model.Test.Services
{
    [TestClass]
    public class ProjectDetailsServiceTest
    {
        [TestMethod]
        public void TestSave()
        {
            var service = ObjectFactory.Instance.GetProjectDetailsService();

            service.Save("test", new ProjectDetails()
                {
                    BranchName = "master",
                    Description = "Projekt testowy",
                    Name = "Projekt"
                });
        }

        [TestMethod]
        public void TestRead()
        {
            var service = ObjectFactory.Instance.GetProjectDetailsService();

            var projectDetails = service.Read("test");
        }
    }
}
