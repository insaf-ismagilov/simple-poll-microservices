namespace SimplePoll.Editor.Application.Models.Requests
{
	public class UpdatePollRequest : SavePollRequest
	{
		public int Id { get; set; }
	}
}