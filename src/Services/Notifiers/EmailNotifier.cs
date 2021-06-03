using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;
using DomainModel.Services;
using DomainModel.Settings;
using Microsoft.Extensions.Logging;

namespace Services.Notifiers
{
    public class EmailNotifier: INotifier, IEmailNotifier
    {
        private readonly IEmailNotifierSettings _settings;
        private readonly IEmailSubscriberRepository _repository;
        private readonly ILogger<EmailNotifier> _log;

        private SmtpClient _smtpClient;

        public EmailNotifier(IEmailNotifierSettings settings,
            IEmailSubscriberRepository repository,
            ILogger<EmailNotifier> log)
        {
            _settings = settings;
            _repository = repository;
            _log = log;

            _smtpClient = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Email, _settings.Password),
                EnableSsl = _settings.EnableSsl
            };
        }

        public async Task Notify(IReadOnlyCollection<ICar> newCars)
        {
            var subscribers = await _repository.GetEnabled();



            var subject = "<h1>New cars available</h1>";

            var sb = new StringBuilder();

            sb.AppendLine("New Hyundai cars are available!");

            foreach (var car in newCars)
            {
                sb.AppendLine($"Model: {car.ModelName}, price {car.Price}");
            }

            var body = sb.ToString();

            foreach (var subscriber in subscribers)
            {
                try
                {
                    var mailMessage = new MailMessage(_settings.Email, subscriber.EMail, subject, body);

                    _smtpClient.Send(mailMessage);
                }
                catch (Exception e)
                {
                    _log.LogError($"Send email exception: {e}");
                }
            }
        }

        public Task SendTestEmail(string to)
        {
            var mailMessage = new MailMessage(_settings.Email, to, "Test Email", "This is a test email");

            _smtpClient.Send(mailMessage);

            return Task.CompletedTask;
        }
    }
}