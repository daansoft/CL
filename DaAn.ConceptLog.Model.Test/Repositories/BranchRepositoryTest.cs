using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DaAn.ConceptLog.Model.Repositories;
using DaAn.ConceptLog.Model.Entities;

namespace DaAn.ConceptLog.Model.Test.Repositories
{
    [TestClass]
    public class BranchRepositoryTest
    {
        [TestMethod]
        public void TestSave()
        {
            var repository = ObjectFactory.Instance.GetBranchRepository();
            ProjectSettings.Path = "Test";
            repository.Save(new Branch()
            {
                Name = "master",
                CommitId = Guid.NewGuid().ToString(),
            });
        }

        [TestMethod]
        public void TestFindAll()
        {
            var repository = ObjectFactory.Instance.GetBranchRepository();

            ProjectSettings.Path = "Test";

            var result = repository.FindAll();
        }
    }
}
