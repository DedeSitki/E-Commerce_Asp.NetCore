namespace Yurukcu.Web.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreatedDate{ get; set; }
    }
}
