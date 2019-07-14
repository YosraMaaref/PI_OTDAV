using System;


namespace PI_OTDAV_Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        RepositoryBase<T> getRepository<T>() where T : class;
    }

}
