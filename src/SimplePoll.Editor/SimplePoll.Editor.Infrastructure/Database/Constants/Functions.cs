namespace SimplePoll.Editor.Infrastructure.Database.Constants
{
	public static class Functions
	{
		public static class Poll
		{
			public const string Create = "public.polls_create";
			public const string Update = "public.polls_update";
			public const string GetById = "public.polls_get_by_id";
			public const string GetAll = "public.polls_get_all";
		}
	}
}