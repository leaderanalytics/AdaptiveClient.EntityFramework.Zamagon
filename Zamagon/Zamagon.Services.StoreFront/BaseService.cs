namespace Zamagon.Services.StoreFront;

public abstract class BaseService : IDisposable
{
    protected ISFServiceManifest ServiceManifest;
    protected Db db;

    public BaseService(Db db, ISFServiceManifest serviceManifest)
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
