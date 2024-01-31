using System.ComponentModel.DataAnnotations;

namespace RedisApp.Database.Entities
{
    public class Orderitems
    {
        [Key]
        public Guid OrderitemUid { get; set; }
        //public Guid OrderUid { get; set; }
        public String? ItemName { get; set; }    //its nullable because of string.
        public int Quantity { get; set; }
        public bool isActive { get; set; }
        public DateTime placedOn { get; set; }
        public string? OrderStatus { get; set; }

        //relationship
        public Order? Order { get; set; }

    }
}
