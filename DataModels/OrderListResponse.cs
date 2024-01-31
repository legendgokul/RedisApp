using RedisApp.Database.Entities;

namespace RedisApp.DataModels
{
    public class OrderListResponse
    {
        public Guid OrderUid { get; set; }
        public String? Description { get; set; }
        public int itemCount { get; set; }
        public Boolean isActive { get; set; }
        public Guid UserId { get; set; }
        public List<Items> Items { get; set; }
    }

    public class Items
    {
        public Guid OrderitemUid { get; set; }
        //public Guid OrderUid { get; set; }
        public String? ItemName { get; set; }    //its nullable because of string.
        public int Quantity { get; set; }
        public bool isActive { get; set; }
        public DateTime placedOn { get; set; }
        public string? OrderStatus { get; set; }


    }
}
