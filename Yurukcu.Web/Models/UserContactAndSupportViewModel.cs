using System.ComponentModel.DataAnnotations;

namespace Yurukcu.Web.Models
{
    public class UserContactAndSupportViewModel
    {
        [Required(ErrorMessage ="Adınız gereklidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="E-Posta adresiniz gereklidir")]
        [EmailAddress(ErrorMessage ="Geçerli bir E-Posta adresi giriniz.")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Mesajınız gereklidir")]
        public string Message { get; set; }
    }
}
