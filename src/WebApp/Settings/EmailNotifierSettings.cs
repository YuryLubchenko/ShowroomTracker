using DomainModel.Settings;

namespace WebApp.Settings
{
    public class EmailNotifierSettings: IEmailNotifierSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool EnableSsl { get; set; }
    }
}