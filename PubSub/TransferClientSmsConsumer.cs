using System;
using PubSub.Events.Model;

namespace PubSub
{
	public class TransferReceiverSmsConsumer : IConsumer<Event<Transfer>>
	{ 
		public int Order => 2;

		public void Handle(Event<Transfer> eventMessage)
		{ 
			var representation = $"Sms : {eventMessage.Entity.Sender} transferred {eventMessage.Entity.Amount} TL. to {eventMessage.Entity.Receiver} on {eventMessage.Entity.Now}.";
			Console.WriteLine(representation);
		}
	}
}
