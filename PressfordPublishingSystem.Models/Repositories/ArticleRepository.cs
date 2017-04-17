using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Models
{
    public class ArticleRepository : EntityRepository<Article>, IArticleRepository
    {
        public ArticleRepository(DbContext db) : base(db)
        {

        }

        public override IQueryable<Article> Read()
        {
            return base.Read().Include(a => a.Author)
                              .Include(a => a.Likes);
        }

        public override Article Find(int id)
        {
            return this.Read().FirstOrDefault(a => a.Id == id);
        }

        public virtual void Create(Article article, string authorId)
        {
            article.AuthorId = authorId;
            base.Create(article);
        }
    }
}
