using System.ComponentModel.DataAnnotations;

namespace Yurukcu.Web.Models
{
    public class UserLogInViewModel
    {
        [EmailAddress(ErrorMessage = "Geçerli bir E-Posta adresi giriniz")]
        [Required]
        public string EMail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
