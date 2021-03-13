namespace SimplePoll.Common.Models
{
	public class ServiceResponse
	{
		public ServiceResponse(bool successful, string errorMessage)
		{
			(Successful, ErrorMessage) = (successful, errorMessage);
		}

		public bool Successful { get; }
		public string ErrorMessage { get; }

		public static ServiceResponse Success()
		{
			return new(true, null);
		}

		public static ServiceResponse Error(string message = null)
		{
			return new(false, message);
		}
	}

	public class ServiceResponse<T> : ServiceResponse
	{
		public ServiceResponse(bool successful, T data, string errorMessage) : base(successful, errorMessage)
		{
			Data = data;
		}

		public T Data { get; }

		public static ServiceResponse<T> Success(T data = default)
		{
			return new(true, data, null);
		}

		public static ServiceResponse<T> Error(string message = null, T data = default)
		{
			return new(false, data, message);
		}
	}
}