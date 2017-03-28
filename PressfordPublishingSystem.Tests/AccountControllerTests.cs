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
    public class AccountControllerTests
    {
        [TestMethod]
        public void LoginReturnsViewAndPassesReturnUrl()
        {
            var controller = new AccountController();
            var returnUrl = "test";

            var result = controller.Login(returnUrl);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(returnUrl, ((ViewResult)result).ViewBag.ReturnUrl);
        }
    }
}
