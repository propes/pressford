using PressfordPublishingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Mappers
{
    public class ArticleDisplayMapper : ModelMapper<Article, ArticleDisplayViewModel>
    {
        public override void MapToModel(ArticleDisplayViewModel viewModel, Article model)
        {
            throw new InvalidOperationException("View model is read-only");
        }

        public override void MapToViewModel(Article model, ArticleDisplayViewModel viewModel)
        {
            viewModel.Id = model.Id;
            viewModel.Title = model.Title;
            viewModel.Body = model.Body.Replace("\n", "<br />");
            viewModel.Author = model.Author.UserName;
            viewModel.DatePublished = model.DatePublished;
            viewModel.DateModified = model.DateModified;
            viewModel.LikesCount = model.Likes.Count;
        }
    }
}
