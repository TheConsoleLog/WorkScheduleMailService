using System;
using System.Net;
using System.Net.Mail;
using AutoRoasterEmailWorker.Configurations;
namespace AutoRoasterEmailWorker
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(SmtpSettings smtpSettings, ILogger<EmailSender> logger)
        {
            _smtpSettings = smtpSettings;
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port ?? 25)
            {
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = true
            };

            _logger.LogTrace($"Sending an email to {email} with subject {subject}");

            return client.SendMailAsync(new MailMessage()
            {
                From = new MailAddress(_smtpSettings.FromEmail, _smtpSettings.FromName),
                To = { new MailAddress(email) },
                Subject = subject,
                Body = message
            });
        }
    }
}

