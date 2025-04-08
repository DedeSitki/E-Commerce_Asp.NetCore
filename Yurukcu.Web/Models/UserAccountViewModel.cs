using System.ComponentModel.DataAnnotations;

namespace Yurukcu.Web.Models
{
    public class UserAccountViewModel
    {
        [EmailAddress]
        public string EMail { get; set; }


        public DateTime CreatedDate { get; set; }
    }
}
