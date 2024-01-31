using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisApp.DataAccess.Interface;
using RedisApp.Database.Context;
using RedisApp.Database.Entities;
using RedisApp.DataModels;
using StackExchange.Redis;
using System.Data;
using Order = RedisApp.Database.Entities.Order;

namespace RedisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly TestingContext _context;
        public IOrderRepo _orderitemsWithOrder;
        public ValuesController(TestingContext test, IOrderRepo orderitemsWithOrder)
        {
            _context = test;
            _orderitemsWithOrder = orderitemsWithOrder;
        }

        /// <summary>
        /// Insert Seed Order information into order and orderitem tables
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpPut("FetchAllOrders")]
        public async Task<ActionResult<Boolean>> FetchAllOrders(int count)
        {
            List<Order> orders = await _orderitemsWithOrder.GetAllOrder();
            try
            {
                Random rand = new Random();

                for (int i = 1; i <= count; i++)
                {
                    var order = new Order(); // Create a new Order instance for each iteration

                    order.Description = "random text";
                    order.itemCount = rand.Next(0, 5);
                    order.isActive = true;
                    order.UserId = Guid.NewGuid();
                    order.Items = new List<Orderitems>();

                    for (int j = 1; j <= order.itemCount; j++)
                    {
                        var orderItem = new Orderitems();
                        orderItem.ItemName = "random name" + j;
                        orderItem.Quantity = rand.Next(0, 5);
                        orderItem.isActive = true;
                        orderItem.placedOn = DateTime.UtcNow;
                        orderItem.OrderStatus = "inprogress";               
                        order.Items.Add(orderItem);
                    }

                    await _context.Orders.AddAsync(order);
                }

                await _context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                throw ex; 
            }
            return Ok( true);
        }

        /// <summary>
        /// Fetch all Orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet("FetchAllOrders")]
        public async Task<ActionResult<List<OrderListResponse>>> FetchAllOrders()
        {
            try
            {
                // Assuming _orderitemsWithOrder is an instance of a service or repository
                List<Order> orders = await _orderitemsWithOrder.GetAllOrder();
                List<OrderListResponse> responseList = new List<OrderListResponse>();
                foreach (var order in orders)
                {
                    var o = new OrderListResponse();
                    o.Description = order.Description; 
                    o.itemCount = order.itemCount;
                    o.isActive = order.isActive;
                    o.UserId = order.UserId;
                    o.Items = new List<Items>();

                    foreach (var oi in order.Items)
                    {
                        var orderItem = new Items();
                        orderItem.ItemName = oi.ItemName;
                        orderItem.Quantity = oi.Quantity;
                        orderItem.isActive = oi.isActive;
                        orderItem.placedOn = oi.placedOn;
                        orderItem.OrderStatus = oi.OrderStatus;
                        o.Items.Add(orderItem);
                    }
                    responseList.Add(o);
                }

                // Returning HTTP 200 (OK) status along with the list of orders
                return Ok(responseList);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("writeToRedis")]
        public Boolean writeToRedis()
        {
            // Replace "localhost" and 6379 with your Redis server information
            string redisConnectionString = "localhost:6379";

            // Create a connection to the Redis server
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisConnectionString);

            // Get a reference to the Redis database
            IDatabase db = redis.GetDatabase();

            // Key to store the string
            string key = "exampleKey";

            // String value to be stored in Redis
            string value = "Hello, Redis!";

            // Write the string value into Redis
            db.StringSet(key, value);

            // Optionally, you can set an expiration time for the key
            // db.KeyExpire(key, TimeSpan.FromMinutes(10));

            Console.WriteLine($"String value '{value}' written to Redis with key '{key}'.");

            // Close the connection when done
            redis.Close();
            return true;
        }

    }
}
