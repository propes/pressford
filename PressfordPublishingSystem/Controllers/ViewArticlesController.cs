using Microsoft.AspNet.Identity;
using PressfordPublishingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PressfordPublishingSystem.Controllers
{
    [Authorize]
    public class ViewArticlesController : Controller
    {
        private readonly IArticleRepository _repo;
        private readonly IModelMapper<Article, ArticleDisplayViewModel> _mapper;

        public ViewArticlesController(IArticleRepository repo, IModelMapper<Article, ArticleDisplayViewModel> mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ActionResult Index(DateTime? monthFilter = null)
        {
            var articles = _repo.Read().Where(a => monthFilter == null
                                                   ||
                                                  (a.DatePublished.Month == monthFilter.Value.Month &&
                                                   a.DatePublished.Year == monthFilter.Value.Year))
                                       .OrderByDescending(a => a.DatePublished)
                                       .ToList();

            var allArticles = _repo.Read().ToList();
            var publicationMonths = allArticles.Select(a => a.DatePublished.FirstDayOfMonth()).Distinct().OrderBy(m => m);
            var publicationMonthsByYear = publicationMonths.GroupBy(m => m.Year).ToList();

            var viewModel = new ViewArticlesViewModel
            {
                Articles = articles.Select(a => _mapper.MapToViewModel(a)).ToList(),
                PublicationMonthsByYear = publicationMonthsByYear
            };

            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (_repo != null)
                _repo.Dispose();

            base.Dispose(disposing);
        }
    }
}