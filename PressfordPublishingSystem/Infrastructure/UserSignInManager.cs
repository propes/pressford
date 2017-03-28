using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PressfordPublishingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem
{
    public class UserSignInManager : SignInManager<User, string>
    {
        public UserSignInManager(UserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserManager)UserManager);
        }

        public static UserSignInManager Create(IdentityFactoryOptions<UserSignInManager> options, IOwinContext context)
        {
            return new UserSignInManager(context.GetUserManager<UserManager>(), context.Authentication);
        }
    }
}
