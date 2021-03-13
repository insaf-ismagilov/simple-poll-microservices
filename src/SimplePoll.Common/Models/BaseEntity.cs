namespace SimplePoll.Common.Models
{
	public abstract class BaseEntity<T>
	{
		public T Id { get; init; }
	}
}