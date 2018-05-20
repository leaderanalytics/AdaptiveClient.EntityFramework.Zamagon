using System;
using System.Collections.Generic;
using System.Text;
using Zamagon.Domain.BackOffice;
using Zamagon.Services.BackOffice.Database;

namespace Zamagon.Services.BackOffice
{
    public abstract class BaseService : IDisposable
    {
        protected IBOServiceManifest ServiceManifest;
        protected Db db;

        public BaseService(Db db, IBOServiceManifest serviceManifest)
        {
            this.db = db;
            this.ServiceManifest = serviceManifest;
        }


        #region IDisposable
        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    disposed = true;
                    db.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
