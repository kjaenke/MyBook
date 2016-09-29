using System.Collections.Generic;
using TestSite.Models;

namespace TestSite.Persistence.Repository.IRepository
{
    internal interface IArticleRepository
    {
        IEnumerable<Article> GetArticle();
        void DeleteArticleById(int articleId);
    }
}