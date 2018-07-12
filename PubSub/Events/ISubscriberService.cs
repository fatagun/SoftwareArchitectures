using System.Collections.Generic;

namespace PubSub
{
    public interface ISubscriberService
    {
        IList<IConsumer<T>> GetSubscribers<T>();
    }
}
