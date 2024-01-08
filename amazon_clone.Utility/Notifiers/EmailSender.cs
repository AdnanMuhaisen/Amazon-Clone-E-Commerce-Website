using Microsoft.AspNetCore.Identity.UI.Services;

namespace amazon_clone.Utility.Notifiers
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
