namespace SimplePoll.Editor.Application.Commands
{
	public class UpdatePollCommand : SavePollCommand<int?>
	{
		public int Id { get; set; }
	}
}