namespace PubSub
{
	public interface IConsumer<in T>
	{
		void Handle(T eventMessage);
		int Order { get; }
	}
}