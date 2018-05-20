using System;
using LeaderAnalytics.AdaptiveClient;
using System.Net.Http;
using Zamagon.Domain;

namespace Zamagon.Services.Common
{
    public abstract class BaseHTTPService : IDisposable
    {
        protected HttpClient httpClient { get; private set; }
        protected IEndPointConfiguration endPoint;

        public BaseHTTPService(Func<IEndPointConfiguration> endPointFactory)
        {
            endPoint = endPointFactory();

            if (endPoint.EndPointType == EndPointType.HTTP)
                httpClient = new HttpClient { BaseAddress = new Uri(endPoint.ConnectionString) };
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
                    httpClient?.Dispose();
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
