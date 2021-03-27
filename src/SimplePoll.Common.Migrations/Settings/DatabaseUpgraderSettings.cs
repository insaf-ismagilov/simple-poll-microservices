using System.Collections.Generic;
using DbUp.Support;

namespace SimplePoll.Common.Migrations.Settings
{
	public class DatabaseUpgraderSettings
	{
		public ConnectionSettings ConnectionSettings { get; set; }
		public bool DropDatabase { get; set; }
		public ICollection<PathSettings> PathSettings { get; set; }
	}
}