using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Models
{
    public class ViewArticlesViewModel
    {
        public IEnumerable<ArticleDisplayViewModel> Articles { get; set; }
        public IEnumerable<IGrouping<int, DateTime>> PublicationMonthsByYear { get; set; }
    }
}
