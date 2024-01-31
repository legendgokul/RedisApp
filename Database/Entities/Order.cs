using System.ComponentModel.DataAnnotations;

namespace RedisApp.Database.Entities
{
    public class Order
    {
        [Key]  //setting it as primary key.
        public Guid OrderUid { get; set; }  
        public String? Description { get; set; }
        public int itemCount { get; set; }
        public Boolean isActive { get; set; }
        public Guid UserId { get; set; }

        // relationships :
        public ICollection<Orderitems>? Items { get; set; }
    }
}
