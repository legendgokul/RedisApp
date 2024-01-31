using RedisApp.Database.Entities;

namespace RedisApp.DataAccess.Interface
{
    public interface IOrderRepo
    {
        public Task<List<Order>> GetAllOrder();
    }
}
