using System.ComponentModel.DataAnnotations;
using Yurukcu.Web.Validators;

namespace Yurukcu.Web.Models
{
    public class UserMyProfileViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage ="Ad soyad bilgisi giriniz")]
        [MaxLength(40)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Cinsiyet bilgisi giriniz")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Telefon Numarası bilgisi giriniz")]
        public string PhoneNumber { get; set; }

        [BirthdateAttribute]
        [Required(ErrorMessage ="Doğum tarihi bilgisi giriniz")]
        public DateTime Birthday { get; set; }
    }
}
