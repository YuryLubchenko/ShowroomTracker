namespace DomainModel.Settings
{
    public interface IEmailNotifierSettings
    {
        string Host { get; }

        int Port { get; }

        string Email { get; }

        string Password { get; }

        bool EnableSsl { get; }
    }
}