using DbUp.Support;

namespace SimplePoll.Common.Migrations.Settings
{
	public class PathSettings
	{
		public string Path { get; set; }
		public ScriptType ScriptType { get; set; }
		public int RunGroupOrder { get; set; }
	}
}