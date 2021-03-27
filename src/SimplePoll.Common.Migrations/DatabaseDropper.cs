using System;
using Npgsql;
using SimplePoll.Common.Migrations.Settings;

namespace SimplePoll.Common.Migrations
{
	public class DatabaseDropper
	{
		private readonly ConnectionSettings _connectionSettings;
		
		public DatabaseDropper(ConnectionSettings connectionSettings)
		{
			_connectionSettings = connectionSettings;
		}

		public void DropDatabase()
		{
			Console.WriteLine("Dropping database...");
			var defaultDbConnectionString = _connectionSettings.DefaultDatabaseConnectionString;

			using var con = new NpgsqlConnection(defaultDbConnectionString);
			con.Open();

			var dropCommand = new NpgsqlCommand
			{
				Connection = con,
				CommandText = $@"SELECT pg_terminate_backend(pg_stat_activity.pid)
								FROM pg_stat_activity
								WHERE pg_stat_activity.datname = '{_connectionSettings.Database}';
								
								drop database if exists ""{_connectionSettings.Database}"";"
			};
			dropCommand.ExecuteNonQuery();
			Console.WriteLine("Database was successfully dropped.");
		}
	}
}