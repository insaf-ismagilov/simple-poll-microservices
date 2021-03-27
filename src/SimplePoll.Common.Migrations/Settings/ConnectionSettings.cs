namespace SimplePoll.Common.Migrations.Settings
{
	public class ConnectionSettings
	{
		public string Server { get; set; }
		public string DefaultDatabase { get; set; }
		public string Database { get; set; }
		public string User { get; set; }
		public string Password { get; set; }
		public int WaitTimeoutSeconds { get; set; }

		public string HostConnectionString => $"Server={Server}; User Id={User}; Password={Password};";
		public string DatabaseConnectionString => $"Server={Server}; Database={Database}; User Id={User}; Password={Password};";
		public string DefaultDatabaseConnectionString => $"Server={Server}; Database={DefaultDatabase}; User Id={User}; Password={Password};";
	}
}