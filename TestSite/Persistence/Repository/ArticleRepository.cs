using System;
using System.Collections.Generic;
using System.Data.Entity;
using TestSite.Models;
using TestSite.Persistence.Repository.IRepository;

namespace TestSite.Persistence.Repository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Article> GetArticle()
        {
            throw new NotImplementedException();
        }

        public void DeleteArticleById(int articleId)
        {
            throw new NotImplementedException();
        }
    }
}