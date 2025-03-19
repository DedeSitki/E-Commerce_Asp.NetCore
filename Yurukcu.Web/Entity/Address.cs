namespace Yurukcu.Web.Entity
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressTitle { get; set; }
        public string DeliveryAddress { get; set; }
        public bool IsBillingAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
    }
}
