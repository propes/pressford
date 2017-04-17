using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PressfordPublishingSystem.Controllers;
using PressfordPublishingSystem.Mappers;
using PressfordPublishingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PressfordPublishingSystem.Tests
{
    [TestClass]
    public class ArticleMapperTests
    {
        [TestMethod]
        public void MapToViewModelMapsAllProperties()
        {
            // Arrange
            var article = new Article
            {
                Id = 1,
                Title = "Test Title",
                Body = "Test Body",
                Author = new User { UserName = "Test User" },
                DatePublished = new DateTime(2017, 1, 1),
                DateModified = new DateTime(2017, 1, 2)
            };
            var mapper = new ArticleMapper();

            // Act
            var viewModel = new ArticleViewModel();
            mapper.MapToViewModel(article, viewModel);

            // Assert
            Assert.AreEqual(article.Id, viewModel.Id);
            Assert.AreEqual(article.Title, viewModel.Title);
            Assert.AreEqual(article.Body, viewModel.Body);
            Assert.AreEqual(article.Author.UserName, viewModel.Author);
            Assert.AreEqual(article.DatePublished, new DateTime(2017, 1, 1));
            Assert.AreEqual(article.DateModified, new DateTime(2017, 1, 2));
        }

        [TestMethod]
        public void MapToModelUpdatesTitleAndBodyOnly()
        {
            // Arrange
            var viewModel = new ArticleViewModel
            {
                Id = 1,
                Title = "Test Title",
                Body = "Test Body",
                Author = "Test Author",
                DatePublished = new DateTime(2017, 1, 1),
                DateModified = new DateTime(2017, 1, 2)
            };
            var mapper = new ArticleMapper();

            // Act
            var model = new Article();
            mapper.MapToModel(viewModel, model);

            // Assert
            Assert.AreEqual(default(int), model.Id);
            Assert.AreEqual(viewModel.Title, model.Title);
            Assert.AreEqual(viewModel.Body, model.Body);
            Assert.IsNull(model.Author);
            Assert.AreEqual(default(DateTime), model.DatePublished);
            Assert.AreNotEqual(default(DateTime), model.DateModified);
        }

        [TestMethod]
        public void MapToModelUpdatesDatePublishedWhenCreatingNewModel()
        {
            // Arrange
            var viewModel = new ArticleViewModel
            {
                Id = 1,
                Title = "Test Title",
                Body = "Test Body",
                Author = "Test Author",
                DatePublished = new DateTime(2017, 1, 1),
                DateModified = new DateTime(2017, 1, 2)
            };
            var mapper = new ArticleMapper();

            // Act
            var model = mapper.MapToModel(viewModel);

            // Assert
            Assert.AreNotEqual(default(DateTime), model.DatePublished);
        }
    }
}
