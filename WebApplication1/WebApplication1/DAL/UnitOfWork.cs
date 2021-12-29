using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class UnitOfWork : IDisposable
    {
        private MyDbContext context = new();
        private GenericRepository<User> userRepo;
        private GenericRepository<MyRecord> recordRepo;

        public GenericRepository<User> UserRepo
        {
            get
            {
                if (this.userRepo == null)
                {
                    this.userRepo = new GenericRepository<User>(context);
                }
                return userRepo;
            }
        }

        public GenericRepository<MyRecord> RecordRepo
        {
            get
            {
                if (this.recordRepo == null)
                {
                    this.recordRepo = new GenericRepository<MyRecord>(context);
                }
                return recordRepo;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
