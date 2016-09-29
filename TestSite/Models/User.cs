using System.ComponentModel.DataAnnotations;

namespace TestSite.Models
{
    public class User
    {


        [Key]
        public int Id { get;  set; }

        public string Salutation { get;  set; }
        public string Firstname { get;  set; }

        [Required]
        public string Lastname { get;  set; }

        [Required]
        public string Mail { get;  set; }

        public string Phonenumber { get;  set; }

        [Required]
        public string Password { get; set; }

        public string Roles { get; set; }
    }
}