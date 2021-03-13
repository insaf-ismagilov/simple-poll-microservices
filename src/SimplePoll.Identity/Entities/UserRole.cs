using System;
using SimplePoll.Common.Models;
using SimplePoll.Identity.Enums;

namespace SimplePoll.Identity.Entities
{
	public class UserRole : BaseEntity<UserRoleId>
	{
		public string Name { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LastModifiedDate { get; set; }
	}
}