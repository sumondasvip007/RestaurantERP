using System;


namespace DAL.Repository
{
    public class SPRepository : IDisposable
    {
        private RestaurantEntities context = new RestaurantEntities();
        public SPRepository(RestaurantEntities context)
        {
            context = this.context;
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
