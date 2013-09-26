using DaAn.ConceptLog.Model.Repositories;
using DaAn.ConceptLog.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaAn.ConceptLog.Model
{
    public class ObjectFactory
    {
        public static ObjectFactory Instance;

        static ObjectFactory()
        {
            ObjectFactory.Instance = new ObjectFactory();
        }

        protected ObjectFactory()
        {
        }

        private ProjectDetailsRepository projectDetailsRepository;
        public ProjectDetailsRepository GetProjectDetailsRepository()
        {
            if (this.projectDetailsRepository == null)
            {
                this.projectDetailsRepository = new ProjectDetailsRepository();
            }
            return this.projectDetailsRepository;
        }

        private CommitRepository commitRepository;
        public CommitRepository GetCommitRepository()
        {
            if (this.commitRepository == null)
            {
                this.commitRepository = new CommitRepository();
            }
            return this.commitRepository;
        }

        private UserRepository userRepository;
        public UserRepository GetUserRepository()
        {
            if (this.userRepository == null)
            {
                this.userRepository = new UserRepository();
            }
            return this.userRepository;
        }

        private BranchRepository branchRepository;
        public BranchRepository GetBranchRepository()
        {
            if (this.branchRepository == null)
            {
                this.branchRepository = new BranchRepository();
            }

            return this.branchRepository;
        }

        private SnapshotRepository snapshotRepository;
        public SnapshotRepository GetSnapshotRepository()
        {
            if (this.snapshotRepository == null)
            {
                this.snapshotRepository = new SnapshotRepository();
            }

            return this.snapshotRepository;
        }

        private BlobRepository blobRepository;
        public BlobRepository GetBlobRepository()
        {
            if (this.blobRepository == null)
            {
                this.blobRepository = new BlobRepository();
            }

            return this.blobRepository;
        }


        //-------------------------------------------------

        public ConceptService GetConceptService()
        {
            return new ConceptService(this.GetProjectDetailsRepository(),
                this.GetBranchRepository(),
                this.GetCommitRepository(),
                this.GetSnapshotRepository(),
                this.GetBlobRepository());
        }

        public ProjectDetailsService GetProjectDetailsService()
        {
            return new ProjectDetailsService(this.GetProjectDetailsRepository());
        }

        public BranchService GetBranchService()
        {
            return new BranchService(this.GetBranchRepository());
        }
    }
}
