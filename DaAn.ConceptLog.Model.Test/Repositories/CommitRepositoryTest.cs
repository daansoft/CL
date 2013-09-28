using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DaAn.ConceptLog.Model.Repositories;
using DaAn.ConceptLog.Model.Entities;

namespace DaAn.ConceptLog.Model.Test.Repositories
{
    [TestClass]
    public class CommitRepositoryTest
    {
        [TestMethod]
        public void TestSave()
        {
            var repository = ObjectFactory.Instance.GetCommitRepository();

            ProjectSettings.Path = "Test";
            repository.Save(new Commit()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "Test",
                ParentId = null,
                UserId = Guid.NewGuid()
            });
        }

        [TestMethod]
        public void TestFindAll()
        {
            var repository = ObjectFactory.Instance.GetCommitRepository();
            ProjectSettings.Path = "Test";
            var result = repository.FindAll();
        }
    }
}
