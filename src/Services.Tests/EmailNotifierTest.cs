using DomainModel.Entities;
using DomainModel.Repositories;
using DomainModel.Settings;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Services.Notifiers;

namespace Services.Tests
{
    public class EmailNotifierTest
    {
        private EmailNotifier _emailNotifier;

        [SetUp]
        public void Setup()
        {
            var settingsMock = new Mock<IEmailNotifierSettings>();
            settingsMock.Setup(x => x.Host).Returns("");
            settingsMock.Setup(x => x.Port).Returns(0);
            settingsMock.Setup(x => x.Email).Returns("");
            settingsMock.Setup(x => x.Password).Returns("");
            settingsMock.Setup(x => x.EnableSsl).Returns(false);

            var emailSubscriberMock = new Mock<IEmailSubscriber>();
            emailSubscriberMock.SetupGet(x => x.EMail).Returns("");

            var emailSubscriberRepository = new Mock<IEmailSubscriberRepository>();
            emailSubscriberRepository.Setup(x => x.GetEnabled()).ReturnsAsync(() => new[] {emailSubscriberMock.Object});

            var loggerMock = new Mock<ILogger<EmailNotifier>>();

            _emailNotifier =
                new EmailNotifier(settingsMock.Object, emailSubscriberRepository.Object, loggerMock.Object);
        }

        [Test]
        public void TestNotify()
        {
            var carMock = new Mock<ICar>();
            carMock.SetupGet(x => x.ModelName).Returns("Some Car");
            carMock.SetupGet(x => x.Price).Returns(1000000);

            _emailNotifier.Notify(new[] {carMock.Object});
        }
    }
}