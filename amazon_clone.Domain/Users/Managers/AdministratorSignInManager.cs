using amazon_clone.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace amazon_clone.Domain.Users.Managers
{
    public class AdministratorSignInManager : SignInManager<Administrator>
    {
        public AdministratorSignInManager(
            UserManager<Administrator> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<Administrator> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<Administrator>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<Administrator> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
    }
}
