using Microsoft.VisualStudio.TestTools.UnitTesting;
using PressfordPublishingSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PressfordPublishingSystem.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void IndexReturnsView()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void HelpReturnsView()
        {
            var controller = new HomeController();

            var result = controller.Help();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AboutReturnsView()
        {
            var controller = new HomeController();

            var result = controller.About();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
