using System.ComponentModel.DataAnnotations;

namespace Yurukcu.Web.Models
{
    public class UserCreateViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage ="Geçerli bir EMail giriniz")]
        public string EMail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
    }
}
