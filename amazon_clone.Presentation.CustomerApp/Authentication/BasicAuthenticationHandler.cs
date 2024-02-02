using amazon_clone.Domain.Users.CurrentUsers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace amazon_clone.Presentation.CustomerApp.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!(Request.Headers.ContainsKey("Authorization")))
                return Task.FromResult(AuthenticateResult.NoResult());

            if (!(AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"],out var authenticationHeaderValue)))
                return Task.FromResult(AuthenticateResult.Fail("Unknown Scheme"));

            ArgumentNullException.ThrowIfNull(authenticationHeaderValue);

            var UserIdAndEmail = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationHeaderValue.Parameter!)).Split(':');

            if (!(UserIdAndEmail[0] == CurrentCustomer.UserID) || !(UserIdAndEmail[1]==CurrentCustomer.Email))
                return Task.FromResult(AuthenticateResult.Fail("Unknown User"));

            // the user is authorized
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,CurrentCustomer.UserID),
                new Claim(ClaimTypes.Email,CurrentCustomer.Email)
            }, authenticationHeaderValue.Scheme);

            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, authenticationHeaderValue.Scheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
