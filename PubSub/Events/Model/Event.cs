using System;
namespace PubSub.Events.Model
{
	public class Event<T> where T: IEntity
    {
        public Event(T entity)
        {
			Entity = entity;
        }

		public T Entity { get; }
    }
}
