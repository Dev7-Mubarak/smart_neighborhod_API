using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SmartNeighborhoodAPI.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSSL
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Username),
                Subject = subject,
                Body = $"<html><body>{htmlMessage}</body></html>",
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}
