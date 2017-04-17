using PressfordPublishingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Mappers
{
    public class ArticleMapper : ModelMapper<Article, ArticleViewModel>
    {
        public override void MapToModel(ArticleViewModel viewModel, Article model)
        {
            model.Title = viewModel.Title;
            model.Body = viewModel.Body;
            model.DateModified = DateTime.Now;
        }

        public override void MapToViewModel(Article model, ArticleViewModel viewModel)
        {
            viewModel.Id = model.Id;
            viewModel.Title = model.Title;
            viewModel.Body = model.Body;
            viewModel.Author = model.Author.UserName;
            viewModel.DatePublished = model.DatePublished;
            viewModel.DateModified = model.DateModified;
        }

        public override Article MapToModel(ArticleViewModel viewModel)
        {
            var model = base.MapToModel(viewModel);
            model.DatePublished = DateTime.Now;

            return model;
        }
    }
}
