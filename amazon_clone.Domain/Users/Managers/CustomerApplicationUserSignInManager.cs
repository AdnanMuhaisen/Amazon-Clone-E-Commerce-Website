using amazon_clone.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace amazon_clone.Domain.Users.Managers
{
    public class CustomerApplicationUserSignInManager<T> : SignInManager<CustomerApplicationUser>
    {
        public CustomerApplicationUserSignInManager(
            UserManager<CustomerApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<CustomerApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<CustomerApplicationUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<CustomerApplicationUser> userConfirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, userConfirmation)
        {
        }
    }
}
