namespace SimplePoll.Identity.Infrastructure.Database.Constants
{
	public static class Functions
	{
		public static class UserRepository
		{
			public const string GetById = "public.users_get_by_id";
			public const string GetByEmail = "public.users_get_by_email";
			public const string Add = "public.users_add";
			public const string Update = "public.users_update";
		}
	}
}