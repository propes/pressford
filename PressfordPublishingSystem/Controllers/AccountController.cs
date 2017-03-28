using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PressfordPublishingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PressfordPublishingSystem.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private const string InvalidLoginMessage = "Invalid username or password";
        private UserSignInManager _signInManager;

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public UserSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<UserSignInManager>(); }
            private set { _signInManager = value; }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController()
        {

        }

        public AccountController(UserSignInManager signInManager)
        {
            SignInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.UserName, AppConstants.DefaultPassword, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);

                    default:
                        ViewBag.ErrorMessage = InvalidLoginMessage;
                        return View(model);
                }
            }

            ViewBag.ErrorMessage = InvalidLoginMessage;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}