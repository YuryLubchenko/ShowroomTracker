namespace DomainModel.Entities
{
    public interface ITelegramSubscriber
    {
        int Id { get; }

        long ChatId { get; }

        bool Disabled { get; }
    }
}