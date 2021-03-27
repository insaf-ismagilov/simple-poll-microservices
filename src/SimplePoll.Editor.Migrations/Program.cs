using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using SimplePoll.Common.Migrations;
using SimplePoll.Common.Migrations.Settings;

namespace SimplePoll.Editor.Migrations
{
	class Program
	{
		static void Main(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddEnvironmentVariables()
				.Build();
			
			var databaseUpgraderSettings = new DatabaseUpgraderSettings();
			configuration.GetSection(nameof(DatabaseUpgraderSettings)).Bind(databaseUpgraderSettings);

			var migrator = new DatabaseMigrator(databaseUpgraderSettings);

			migrator.TryExecute(Assembly.GetExecutingAssembly());
		}
	}
}