using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PressfordPublishingSystem.Controllers;
using PressfordPublishingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PressfordPublishingSystem.Tests
{
    [TestClass]
    public class PublishArticlesControllerTests
    {
        //private Mock<ArticleRepository> mockRepository;
        private Mock<IModelMapper<Article, ArticleViewModel>> mockMapper;
        private Mock<IArticleRepository> mockRepository;

        [TestInitialize]
        public void Initialise()
        {
            mockRepository = new Mock<IArticleRepository>();
            mockRepository.Setup(m => m.Find(It.IsAny<int>())).Returns<int>(m => new Article { Id = m });

            mockMapper = new Mock<IModelMapper<Article, ArticleViewModel>>();
            mockMapper.Setup(m => m.MapToViewModel(It.IsAny<Article>())).Returns<Article>(m => new ArticleViewModel { Id = m.Id });
            mockMapper.Setup(m => m.MapToModel(It.IsAny<ArticleViewModel>())).Returns<ArticleViewModel>(m => new Article { Id = m.Id });
        }

        private PublishArticlesController GetTestObject()
        {
            var controller = new PublishArticlesController(mockRepository.Object, mockMapper.Object);

            // Mock the user, identity and controller context.
            var identityMock = new Mock<IIdentity>();
            var userMock = new Mock<IPrincipal>();
            userMock.Setup(u => u.Identity).Returns(identityMock.Object);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.Setup(c => c.User).Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.Setup(c => c.HttpContext).Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            return controller;
        }

        [TestMethod]
        public void IndexReturnsView()
        {
            // Arrange
            var controller = new PublishArticlesController(mockRepository.Object, mockMapper.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ArticlesTableReturnsViewWithData()
        {
            // Arrange
            mockRepository.Setup(m => m.Read()).Returns(new[]
            {
                new Article { Id = 2 },
                new Article { Id = 3 },
                new Article { Id = 1 }
            }
            .AsQueryable());

            var controller = new PublishArticlesController(mockRepository.Object, mockMapper.Object);

            // Act
            var result = controller.ArticlesTable() as PartialViewResult;
            var viewModels = result.ViewData.Model as IEnumerable<ArticleViewModel>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, viewModels.Count());
            Assert.AreEqual(2, viewModels.ElementAt(0).Id);
            Assert.AreEqual(3, viewModels.ElementAt(1).Id);
            Assert.AreEqual(1, viewModels.ElementAt(2).Id);
        }

        [TestMethod]
        public void EditReturnsEmptyViewModelIfIdIsZero()
        {
            // Arrange
            var controller = new PublishArticlesController(mockRepository.Object, mockMapper.Object);
            var id = 0;

            // Act
            var result = controller.Edit(id) as ViewResult;
            var viewModel = result.ViewData.Model as ArticleViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(default(int), viewModel.Id);
        }

        [TestMethod]
        public void EditReturnsMappedViewModelIfIdExists()
        {
            // Arrange
            var controller = new PublishArticlesController(mockRepository.Object, mockMapper.Object);
            var id = 2;

            // Act
            var result = controller.Edit(id) as ViewResult;
            var viewModel = result.ViewData.Model as ArticleViewModel;

            // Assert
            Assert.AreEqual(id, viewModel.Id);
        }

        [TestMethod]
        public void EditCreatesNewArticleAndRedirectsIfIdOfViewModelIsZero()
        {
            // Arrange
            var viewModel = new ArticleViewModel { Id = 0 };
            var controller = GetTestObject();

            // Act
            var result = controller.Edit(viewModel) as RedirectToRouteResult;

            // Assert.
            mockMapper.Verify(mapper => mapper.MapToModel(viewModel));
            mockRepository.Verify(repo => repo.Create(It.Is<Article>(a => a.Id == viewModel.Id), It.IsAny<string>()), Times.Once());
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EditUpdatesArticleIfViewModelIdExists()
        {
            // Arrange
            var viewModel = new ArticleViewModel { Id = 1 };
            var controller = GetTestObject();

            // Act
            var result = controller.Edit(viewModel) as ViewResult;
            var resultViewModel = result.ViewData.Model as ArticleViewModel;

            // Assert
            mockRepository.Verify(repo => repo.Update(It.Is<Article>(a => a.Id == viewModel.Id)), Times.Once());
            Assert.AreEqual(viewModel.Id, resultViewModel.Id);
        }

        [TestMethod]
        public void DeleteCallsRepositoryDeleteMethodWithCorrectId()
        {
            // Arrange
            var id = 3;
            var controller = GetTestObject();

            // Act
            var result = controller.Delete(id);

            // Assert
            mockRepository.Verify(repo => repo.Delete(It.Is<Article>(a => a.Id == id)), Times.Once());
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void DeleteReturnsErrorCodeWhenDeleteCouldNotBePerformed()
        {
            // Arrange
            var controller = GetTestObject();
            mockRepository.Setup(m => m.Delete(It.IsAny<Article>())).Throws(new Exception());

            // Act
            var result = controller.Delete(3) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
