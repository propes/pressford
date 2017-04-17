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
    public class PublishArticlesController : Controller
    {
        private readonly IArticleRepository _repo;
        private readonly IModelMapper<Article, ArticleViewModel> _mapper;

        public PublishArticlesController(IArticleRepository repo, IModelMapper<Article, ArticleViewModel> mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }

        public ActionResult ArticlesTable()
        {
            var articles = _repo.Read().ToList();
            var viewModels = articles.Select(a => _mapper.MapToViewModel(a)).ToList();

            return PartialView("ArticlesTable", viewModels);
        }

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                var viewModel = new ArticleViewModel();
                return View(viewModel);
            }
            else
            {
                var article = _repo.Find(id);
                var viewModel = _mapper.MapToViewModel(article);

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(ArticleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Id == 0)
                {
                    var article = _mapper.MapToModel(viewModel);
                    var authorId = User.Identity.GetUserId();
                    _repo.Create(article, authorId);

                    TempData["SuccessMessage"] = "Article Created";
                    return RedirectToAction("Index");
                }
                else
                {
                    var article = _repo.Find(viewModel.Id);
                    _mapper.MapToModel(viewModel, article);
                    _repo.Update(article);

                    ViewBag.SuccessMessage = "Article Saved";
                    return View(viewModel);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _repo.Delete(new Article { Id = id });
                return Json(new { success = true });
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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