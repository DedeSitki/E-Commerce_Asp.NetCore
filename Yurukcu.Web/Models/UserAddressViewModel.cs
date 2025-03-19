using System.ComponentModel.DataAnnotations;

namespace Yurukcu.Web.Models
{
    public class UserAddressViewModel
    {
        public int AddressId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string AddressTitle { get; set; } // Örn: "Ev", "İş"

        [Required]
        public string DeliveryAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public bool IsBillingAddress { get; set; } // True: Fatura, False: Teslimat
    }
}
