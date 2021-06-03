namespace DomainModel.Entities
{
    public interface IEmailSubscriber
    {
        int Id { get; }

        string EMail { get; }

        bool Disabled { get; }
    }
}