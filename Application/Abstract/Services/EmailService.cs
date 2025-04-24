using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Application.Abstract.Services
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string htmlMessage);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(string toEmail, string subject, string htmlMessage)
        {
            var smtp = _config.GetSection("Smtp");

            using var client = new SmtpClient
            {
                Host = smtp["Host"],
                Port = int.Parse(smtp["Port"]!),
                EnableSsl = bool.Parse(smtp["EnableSsl"]!),
                Credentials = new NetworkCredential(smtp["UserName"], smtp["Password"])
            };

            var mail = new MailMessage
            {
                From = new MailAddress(smtp["From"]!),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mail.To.Add(toEmail);
            await client.SendMailAsync(mail);
        }
    }
}
