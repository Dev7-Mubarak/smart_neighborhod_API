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
        private readonly IWebHostEnvironment _environment;

        public EmailSender(IOptions<EmailSettings> emailSettings, ILogger<EmailSender> logger, IWebHostEnvironment environment)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
            _environment = environment;

            // Validate email settings on initialization
            ValidateEmailSettings();
        }

        private void ValidateEmailSettings()
        {
            if (!_emailSettings.IsValid())
            {
                var error = "Email settings are incomplete. Please ensure Host, Port, Username, and Password are configured.";
                _logger.LogError(error);
                throw new InvalidOperationException(error);
            }

            _logger.LogInformation("Email settings validated successfully. Host: {Host}, Port: {Port}, SSL: {EnableSSL}", 
                _emailSettings.Host, _emailSettings.Port, _emailSettings.EnableSSL);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation("Preparing to send email to {Email} with subject '{Subject}'", email, subject);

            try
            {
                using var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
                {
                    Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                    EnableSsl = _emailSettings.EnableSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.Username),
                    Subject = subject,
                    Body = $"<html><body>{htmlMessage}</body></html>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                _logger.LogDebug("Attempting to send email via SMTP. Host: {Host}, Port: {Port}, SSL: {EnableSSL}, Username: {Username}", 
                    _emailSettings.Host, _emailSettings.Port, _emailSettings.EnableSSL, _emailSettings.Username);

                await client.SendMailAsync(mailMessage);

                _logger.LogInformation("Email successfully sent to {Email} with subject '{Subject}'", email, subject);
            }
            catch (SmtpException smtpEx)
            {
                var detailedError = $"SMTP Error: {smtpEx.Message}. Status Code: {smtpEx.StatusCode}";
                
                if (_environment.IsDevelopment())
                {
                    detailedError += $" | Host: {_emailSettings.Host} | Port: {_emailSettings.Port} | SSL: {_emailSettings.EnableSSL} | Username: {_emailSettings.Username}";
                }

                _logger.LogError(smtpEx, "SMTP failed to send email to {Email}. {DetailedError}", email, detailedError);
                
                throw new InvalidOperationException($"Failed to send email: {detailedError}", smtpEx);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Unexpected error sending email to {email}: {ex.Message}";
                
                if (_environment.IsDevelopment())
                {
                    errorMessage += $" | Email Settings - Host: {_emailSettings.Host}, Port: {_emailSettings.Port}, SSL: {_emailSettings.EnableSSL}";
                }

                _logger.LogError(ex, "{ErrorMessage}", errorMessage);
                throw new InvalidOperationException(errorMessage, ex);
            }
        }
    }
}
