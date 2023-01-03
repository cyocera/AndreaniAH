using ApiGeo.Data.Context;
using System;

namespace ApiGeo.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAddressHistoryRepository addressHistory { get; }
        int Save();
    }
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDBContext context;

        public UnitOfWork(AppDBContext context)
        {
            this.context = context;
            addressHistory = new AddressHistoryRepository(this.context);
        }

        public IAddressHistoryRepository addressHistory { get; set; }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
