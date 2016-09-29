namespace TestSite.Models
{
    public class Mail
    {


        public int Id { get;  set; }
        public string Subject { get; set; }
        public string ToId { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public string CreateId { get; set; }
        public string Type { get; set; }
        public string TopId { get;  set; }
        public string Delete { get; set; }
        public string IsRead { get; set; }
    }
}