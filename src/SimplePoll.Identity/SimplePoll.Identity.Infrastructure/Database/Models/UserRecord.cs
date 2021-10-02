using System;

namespace SimplePoll.Identity.Infrastructure.Database.Models
{
	public class UserRecord
	{
		public int Id { get; set; }
		public int RoleId { get; set; }
		public string RoleName { get; set; }
		public DateTime RoleCreatedDate { get; set; }
		public DateTime RoleLastModifiedDate { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LastModifiedDate { get; set; }
	}
}