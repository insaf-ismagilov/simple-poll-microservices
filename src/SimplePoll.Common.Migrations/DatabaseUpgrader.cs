using System.Reflection;
using DbUp;
using DbUp.Engine;
using SimplePoll.Common.Migrations.Settings;

namespace SimplePoll.Common.Migrations
{
	public class DatabaseUpgrader
	{
		private readonly DatabaseUpgraderSettings _upgraderSettings;

		public DatabaseUpgrader(DatabaseUpgraderSettings upgraderSettings)
		{
			_upgraderSettings = upgraderSettings;
		}

		public UpgradeEngine CreateUpgrader(Assembly assembly)
		{
			var connectionString = _upgraderSettings.ConnectionSettings.DatabaseConnectionString;

			var builder = DeployChanges.To.PostgresqlDatabase(connectionString);

			foreach (var pathSetting in _upgraderSettings.PathSettings)
			{
				builder.WithScriptsEmbeddedInAssembly(assembly, script => script.StartsWith(pathSetting.Path),
					new SqlScriptOptions {ScriptType = pathSetting.ScriptType, RunGroupOrder = pathSetting.RunGroupOrder});
			}

			builder.LogToConsole();

			return builder.Build();
		}
	}
}