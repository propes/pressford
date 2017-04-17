using Microsoft.AspNet.Identity;
using PressfordPublishingSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
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
        private readonly int _maxLikes;

        public ViewArticlesController(IArticleRepository repo, IModelMapper<Article, ArticleDisplayViewModel> mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _maxLikes = int.Parse(ConfigurationManager.AppSettings["MaxLikes"]);
        }

        public ViewArticlesController(IArticleRepository repo, IModelMapper<Article, ArticleDisplayViewModel> mapper, int maxLikes)
        {
            _repo = repo;
            _mapper = mapper;
            _maxLikes = maxLikes;
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
            var publicationMonths = allArticles.Select(a => a.DatePublished.FirstDayOfMonth()).Distinct().OrderByDescending(m => m);
            var publicationMonthsByYear = publicationMonths.GroupBy(m => m.Year).ToList();

            var viewModel = new ViewArticlesViewModel
            {
                Articles = articles.Select(a => _mapper.MapToViewModel(a)).ToList(),
                PublicationMonthsByYear = publicationMonthsByYear
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult LikeArticle(int articleId)
        {
            var user = GetCurrentUser();
            var article = _repo.Find(articleId);

            if (user.Likes.Count < _maxLikes)
            {
                article.Likes.Add(new Like { UserId = user.Id });
                _repo.Update(article);

                var likesRemaining = _maxLikes - user.Likes.Count;
                var articleLikes = article.Likes.Count;
                return Json(new { success = true, message = String.Format("{0} likes remaining", likesRemaining), likeCount = articleLikes });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You have no likes remaining!");
            }
        }

        private User GetCurrentUser()
        {
            using (var db = new PublisherDbContext())
            {
                var userId = User.Identity.GetUserId();
                return db.Users.Include(u => u.Likes).FirstOrDefault(u => u.Id == userId);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_repo != null)
                _repo.Dispose();

            base.Dispose(disposing);
        }
    }
}