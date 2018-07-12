
using System;

namespace PubSub{

	public class Transfer : IEntity
	{
		public Transfer(decimal amount, string sender, string receiver)
		{
			Amount = amount;
			Sender = sender;
			Receiver = receiver;
			Now = DateTime.Now;
		}

		public decimal Amount { get; }
		public string Sender { get; }
		public string Receiver { get; }
		public DateTime Now { get; }

		public override string ToString()
		{
			var representation = "{Sender} transferred {Amount} TL. to {Receiver} on {Now}.";
			return representation;
		}

	}
}