namespace Yurukcu.Web.Models
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderAddress { get; set; }
        public string OrderBillingAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderStatus { get; set; }
        public string OrderTrackingCode { get; set; }
    }
}
