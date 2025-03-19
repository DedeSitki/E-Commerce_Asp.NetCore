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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
        ErrorMessage = "Şifre en az 8 karakter olmalı, büyük harf, küçük harf ve rakam içermelidir.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor.")]
        public string RePassword { get; set; }
    }
}
