namespace PubSub
{
    public interface IEventPublisher
    {
        void Publish<T>(T message);
    }
}
