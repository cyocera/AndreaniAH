using ApiGeo.Data.Context;
using ApiGeo.Data.Models;

namespace ApiGeo.Data
{
    public interface IAddressHistoryRepository : IRepository<AddressHistory>
    {
    }
    public class AddressHistoryRepository : Repository<AddressHistory>, IAddressHistoryRepository
    {
        public AddressHistoryRepository(AppDBContext context) : base(context) { }
    }
}
