using System;
using SimplePoll.Common.Models;

namespace SimplePoll.Identity.Domain.Entities
{
	public class User : BaseEntity<int>
	{
		public UserRole Role { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LastModifiedDate { get; set; }
	}
}