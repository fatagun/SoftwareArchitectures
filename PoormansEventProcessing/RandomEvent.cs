using System;
namespace PoormansEventProcessing
{
    public class RandomEvent
    {
        public RandomEvent(string message)
        {
			Message = message;
        }

		public string Message {get;}
    }


	public interface IHandler<T>
    {
        void Handle(T item);
    }
    
    public class Handler1 : IHandler<RandomEvent>
    {

        public void Handle(RandomEvent randomEvent)
        {
            Console.WriteLine("From Handler 1 : {0}", randomEvent.Message);
        }
    }

    public class Handler2 : IHandler<RandomEvent>
    {
		public void Handle(RandomEvent randomEvent)
        {
			Console.WriteLine("From Handler 2 : {0}", randomEvent.Message);
        }
    }

    public class Handler3 : IHandler<RandomEvent>
    {
		public void Handle(RandomEvent randomEvent)
        {
			Console.WriteLine("From Handler 3 : {0}", randomEvent.Message);
        }
    }
}
