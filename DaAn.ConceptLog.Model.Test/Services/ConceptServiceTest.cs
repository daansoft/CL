using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DaAn.ConceptLog.Model.Entities;
using System.Collections.Generic;

namespace DaAn.ConceptLog.Model.Test.Services
{
    [TestClass]
    public class ConceptServiceTest
    {
        [TestMethod]
        public void TestCommit()
        {
            var service = ObjectFactory.Instance.GetConceptService();

            /*service.Commit("test", Guid.NewGuid(), "Testtttt", new ProjectDetails(),
                new List<Concept>() { 
                    new Concept() { 
                        Id = "6648eccc-e586-4397-8ea2-7260f6b05199",
                        Description = "test2"
                    } 
                },
                new List<Concept>(),
                new List<Concept>());*/
        }
        [TestMethod]
        public void TestFindByCommitId()
        {
            var service = ObjectFactory.Instance.GetConceptService();

            var concepts = service.FindByCommitId("test", "1cee363a-162c-460c-a72c-833c24147707");


        }
    }
}
