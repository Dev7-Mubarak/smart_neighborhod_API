using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SmartNeighborhoodAPI.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IOptions<EmailSettings> emailSettings, ILogger<EmailSender> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation("Preparing to send email to {Email} with subject '{Subject}'", email, subject);

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("sys.smartneighborhood@gmail.com", "uoxd ovjz cemn afil"),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("sys.smartneighborhood@gmail.com"),
                    Subject = subject,
                    Body = $"<html><body>{htmlMessage}</body></html>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                 client.SendMailAsync(mailMessage);

                _logger.LogInformation("Email successfully sent to {Email} with subject '{Subject}'", email, subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email} with subject '{Subject}'", email, subject);
                throw;
            }
        }

    }
}
