using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yurukcu.Web.Entity
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderAddress { get; set; }
        public string OrderBillingAddress { get; set; }
        public string[] Orders { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderStatus { get; set; }
        public string OrderTrackingCode { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
