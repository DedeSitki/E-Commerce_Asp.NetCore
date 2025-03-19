namespace Yurukcu.Web.Entity
{
    public class SupportRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Message { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}
