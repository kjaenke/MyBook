using System.ComponentModel.DataAnnotations;

namespace TestSite.Models
{
    public class Forum
    {
   
        [Key]
        public int ForumId { get;  set; }

        [Required]
        public string ForumName { get;  set; }

        [Required]
        public string Status { get;  set; }

        [Required]
        public string Erstelldatum { get; set; }

        [Required]
        public string Comment { get;  set; }

        [Required]
        public string Delete { get; set; }
    }
}