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
        public void IndexReturnsPage()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void HelpReturnsPage()
        {
            var controller = new HomeController();

            var result = controller.Help();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AboutReturnsPage()
        {
            var controller = new HomeController();

            var result = controller.About();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
