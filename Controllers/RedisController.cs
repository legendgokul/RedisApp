using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;

namespace RedisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public RedisController(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }

        [HttpGet("get/{key}")]
        public IActionResult Get(string key)
        {
            IDatabase db = _redisConnection.GetDatabase();
            string value = db.StringGet(key);

            return Ok(value);
        }

        [HttpPost("set/{key}/{value}")]
        public IActionResult Set(string key, string value)
        {
            IDatabase db = _redisConnection.GetDatabase();
            /*
            Default redis behaviour will simply replace the Data present in memory for provided key.
            
            var res =  db.StringSet(key, value);
            */
                      
            //We can add a conditional check to find if there is any value before we replace .
            //below code will only assign value if the key is not already created .        
            var res =  db.StringSet(key, value,when: When.NotExists);

            //the reverse can also be done , replace the value only if the key already exist else dont write anything.
            var res2 = db.StringSet(key, value, when: When.Exists);
            

            return Ok($"Key '{key}' set to value '{value}' in Redis.{res}");
        }

        [HttpPut("setMultipleKey")]
        public IActionResult setMultipleKey()
        {
            IDatabase db = _redisConnection.GetDatabase();
            var res5 = db.StringSet(new KeyValuePair<RedisKey, RedisValue>[]
            {
                new ("bike:1", "Deimos"), new("bike:2", "Ares"), new("bike:3", "Vanth")
            });
        
            var res6 = db.StringGet(new RedisKey[] { "bike:1", "bike:2", "bike:3" });

            var data = res6[0].ToString();

            return Ok($"Data insert completed {res5} and {res6[0]}");
        }

        [HttpPut("SetHashMapTesting")]
        public IActionResult SetHashMapTesting()
        {
            IDatabase db = _redisConnection.GetDatabase();
            db.HashSet("Orders", new HashEntry[]
            {
                new HashEntry("item1","tomato"),
                new HashEntry("item2","potato"),
                new HashEntry("item3","bread"),
                new HashEntry("item4","cheese"),
            });

            var products = db.HashGetAll("Orders");
          /*  var p2 = db.HashGet("Orders", "item1");
            var items1 = products.FirstOrDefault(x => x.Name == "item1").Value;*/
            return Ok($"Data insert completed");
        }

    }
}
