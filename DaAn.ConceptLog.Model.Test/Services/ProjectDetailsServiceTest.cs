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
            ProjectSettings.Path = "test";
            service.Save(new ProjectDetails()
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
            ProjectSettings.Path = "Test";
            var projectDetails = service.Read();
        }
    }
}
