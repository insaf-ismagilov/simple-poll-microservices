namespace SimplePoll.Identity.Application.Options
{
	public class IdentityJwtSettings
	{
		public string Issuer { get; set; }
		public string[] Audiences { get; set; }
		public string SigningKey { get; set; }
		public int LifetimeSeconds { get; set; }
	}
}