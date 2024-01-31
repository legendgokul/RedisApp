using Microsoft.EntityFrameworkCore;
using RedisApp.DataAccess.Interface;
using RedisApp.Database.Context;
using RedisApp.Database.Entities;
using System.Collections.Specialized;
using System.ComponentModel;

namespace RedisApp.DataAccess.Repository
{
    public class OrderRepo : IOrderRepo
    {
        public readonly TestingContext _context;
        public OrderRepo(TestingContext testingContext)
        {
            _context = testingContext;
        }


        public async Task<List<Order>> GetAllOrder()
        {
            return await _context.Orders.Include(o => o.Items).ToListAsync();
        }

    }
}
