using System;
using PubSub.Events.Model;

namespace PubSub
{
	public class TranferTransactionConsumer : IConsumer<Event<Transfer>>
    {
		// inject Customer sender, Customer receiver
        public TranferTransactionConsumer()
        {
			Order = 1;
        }

		public int Order { get; }

		public void Handle(Event<Transfer> eventMessage)
		{ 
			var representation = $"Transaction : {eventMessage.Entity.Sender} transferred {eventMessage.Entity.Amount} TL. to {eventMessage.Entity.Receiver} on {eventMessage.Entity.Now}.";
            Console.WriteLine(representation);
		}
	}
}
