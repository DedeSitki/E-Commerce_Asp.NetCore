using System.ComponentModel.DataAnnotations;

namespace Yurukcu.Web.Validators
{
    public class BirthdateAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value).Date;
            ErrorMessage = "Geçerli bir doğum tarihi seçiniz";
            return dateTime <= DateTime.Now.Date;

        }
    }
}
