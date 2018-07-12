using System;
using System.Linq;

namespace PubSub
{
    public class EventPublisher : IEventPublisher
    {
		private readonly ISubscriberService _subscriberService; 

		public EventPublisher(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService; 
        }

        public virtual void Publish<T>(T message)
        {
			var subscribers = _subscriberService.GetSubscribers<T>();

			foreach(var subscriber in subscribers.OrderBy(s=>s.Order))
			{
				PublishToConsumer(subscriber, message);
			}
        }

        protected virtual void PublishToConsumer<T>(IConsumer<T> consumer, T eventMessage)
        {
            try
            {
                consumer.Handle(eventMessage);
            }
            catch (Exception)
            {
				throw;
            }
        }
    }
}
