using System.ComponentModel.DataAnnotations;

namespace RedisApp.Database.Entities
{
    public class Users
    {
        [Key]
        public Guid UserId { get; set; }
        public String? userName { get; set; }    
        public String password { get;set; }
        public String fullname { get; set; }
        public string address
    }
}
