using System.ComponentModel.DataAnnotations;

namespace TestSite.Models
{
    public class Topic
    {
      
        [Key]
        public int TopicId { get;  set; } //
        public int ForumId { get;  set; } //
        public string Content { get;  set; } //
        public string CreateDate { get; set; } //
        public string Name { get;  set; } //
        public string EditDate { get;  set; }
        public int LastEditId { get;  set; } //
        public int EditCount { get;  set; } //
        public string CreateId { get;  set; } //
        public string Delete { get; set; } //
        public string Blocked { get;  set; } //
        public string Shorttag { get;  set; } //
    }
}