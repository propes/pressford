using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Models
{
    public interface IArticleRepository : IEntityRepository<Article>
    {
        void Create(Article article, string authorId);
    }
}
