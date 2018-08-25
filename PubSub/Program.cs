using System;
using PubSub.Events.Model;

namespace PubSub
{
    class Program
    {
        static void Main(string[] args)
        {
			ISubscriberService subscriberService = new SubscriberService();
			IEventPublisher eventPublisher = new EventPublisher(subscriberService);

			Transfer transfer = new Transfer(120000.0m, "Trump", "Stormy");

			eventPublisher.Publish(new Event<Transfer>(transfer));

            Console.WriteLine("The end.");
            Console.Read();
        }
    }
}
