using amazon_clone.Models.Users;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace amazon_clone.Services.Notification_Services
{
    public class EmailNotificationService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // for demo purposes i will use my personal email but for production will convert 
            // this email to the app outlook email 

            var senderEmail = @"adnanmuhaisen@outlook.com";
            var password = "";

            var smtpClient = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, password)
            };

            return smtpClient.SendMailAsync(
                from: senderEmail,
                recipients: email,
                subject: subject,
                body: htmlMessage
                );
        }
    }
}
