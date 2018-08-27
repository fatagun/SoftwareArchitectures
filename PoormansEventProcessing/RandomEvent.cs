using System;
namespace PoormansEventProcessing
{
    public class SomeEvent
    {
        public SomeEvent(string message)
        {
			Message = message;
        }

		public string Message {get;}
    }


	public interface IHandler<T>
    {
        void Handle(T item);
    }
    
    public class Handler1 : IHandler<SomeEvent>
    {

        public void Handle(SomeEvent randomEvent)
        {
            Console.WriteLine("From Handler 1 : {0}", randomEvent.Message);
        }
    }

    public class Handler2 : IHandler<SomeEvent>
    {
        public void Handle(SomeEvent randomEvent)
        {
			Console.WriteLine("From Handler 2 : {0}", randomEvent.Message);
        }
    }

    public class Handler3 : IHandler<SomeEvent>
    {
        public void Handle(SomeEvent randomEvent)
        {
			Console.WriteLine("From Handler 3 : {0}", randomEvent.Message);
        }
    }
}
