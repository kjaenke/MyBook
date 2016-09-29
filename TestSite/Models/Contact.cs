namespace TestSite.Models
{
    public class Contact
    {


        public int Id { get;  set; }
        public string Firstname { get;  set; }
        public string Lastname { get;  set; }
        public string StreetHouseNumber { get;  set; }
        public string PlzAndOrt { get;  set; }
        public string TelephonNumber { get;  set; }
        public string EMail { get;  set; }
        public string Homepage { get;  set; }
        public int Status { get; set; }
        public int CreateById { get;  set; }
    }
}