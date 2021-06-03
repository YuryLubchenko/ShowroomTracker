namespace DomainModel.Events
{
    public interface IEventConsumer<T>
    {
        void HandleEvent(T e);
    }
}