using System.ComponentModel.DataAnnotations;

namespace TestSite.Models
{
    public class Article
    {
  
        [Key]
        public int ArticleId { get;  set; }

        public int TopicId { get;  set; }
        public string Content { get; set; }
        public string CreateDate { get; set; }
        public string Name { get;  set; }
        public string EditDate { get; set; }
        public int LastEditId { get; set; }
        public int EditCount { get; set; }
        public string CreateId { get;  set; }
        public string Delete { get; set; }
    }
}