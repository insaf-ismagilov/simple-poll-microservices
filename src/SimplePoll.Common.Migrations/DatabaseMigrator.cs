using System;
using System.Reflection;
using System.Threading.Tasks;
using DbUp;
using Npgsql;
using SimplePoll.Common.Migrations.Settings;

namespace SimplePoll.Common.Migrations
{
	public class DatabaseMigrator
	{
		private readonly DatabaseUpgraderSettings _upgraderSettings;
		
		public DatabaseMigrator(DatabaseUpgraderSettings upgraderSettings)
		{
			_upgraderSettings = upgraderSettings;
		}

		public bool TryExecute(Assembly assembly)
		{
			var connectionSettings = _upgraderSettings.ConnectionSettings;
			
			if (!RepeatTryWaitConnection(connectionSettings))
			{
				Console.WriteLine("Connection to the host has failed.");
				return false;
			}

			if (_upgraderSettings.DropDatabase)
			{
				var dropper = new DatabaseDropper(connectionSettings);
				dropper.DropDatabase();
			}
			
			EnsureDatabase.For.PostgresqlDatabase(connectionSettings.DatabaseConnectionString);

			var upgrader = new DatabaseUpgrader(_upgraderSettings);

			var upgradeResult = upgrader.CreateUpgrader(assembly).PerformUpgrade();
			
			if (!upgradeResult.Successful)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(upgradeResult.Error);
				Console.ResetColor();
				return false;
			}
			
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Success!");
			Console.ResetColor();

			return true;
		}
		
		private static bool TryWaitConnection(ConnectionSettings connectionSettings)
		{
			Console.WriteLine("Trying to connect to the host...");
			
			var hostConnectionString = connectionSettings.HostConnectionString;

			try
			{
				using var con = new NpgsqlConnection(hostConnectionString);
				con.Open();
				Console.WriteLine("Succesful connection to the host has been established.");
				return true;
			}
			catch
			{
				return false;
			}
		}

		private static bool RepeatTryWaitConnection(ConnectionSettings connectionSettings)
		{
			const int tryCount = 5;

			for (var i = 0; i < tryCount; i++)
			{
				if (TryWaitConnection(connectionSettings))
					return true;

				Task.Delay(TimeSpan.FromSeconds(connectionSettings.WaitTimeoutSeconds)).Wait();
			}

			return false;
		}
	}
}