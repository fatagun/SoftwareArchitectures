using System;
using PubSub.Events.Model;

namespace PubSub
{
	public class TranferTransactionConsumer : IConsumer<Event<Transfer>>
    {  
        public int Order  => 1;

		public void Handle(Event<Transfer> eventMessage)
		{ 
			var representation = $"Transaction : {eventMessage.Entity.Sender} transferred {eventMessage.Entity.Amount} TL. to {eventMessage.Entity.Receiver} on {eventMessage.Entity.Now}.";
            Console.WriteLine(representation);
		}
	}
}
