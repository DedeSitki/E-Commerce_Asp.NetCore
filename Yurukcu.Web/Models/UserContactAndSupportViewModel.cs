using System.ComponentModel.DataAnnotations;

public class UserContactAndSupportViewModel
{
    [Required(ErrorMessage = "Ad zorunludur.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Mesaj boş bırakılamaz.")]
    public string Message { get; set; }
}
