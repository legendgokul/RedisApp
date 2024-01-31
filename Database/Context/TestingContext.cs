using Microsoft.EntityFrameworkCore;
using RedisApp.Database.Entities;

namespace RedisApp.Database.Context
{
    public class TestingContext : DbContext
    {
        public TestingContext(DbContextOptions<TestingContext> options ): base (options)
        {

        }

        // Table declaration .
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Orderitems>? Ordersitems { get; set; }
        public DbSet<Users>? Users { get; set; }

    }
}
