using System;
using SimplePoll.Common.Models;
using SimplePoll.Identity.Domain.Enums;

namespace SimplePoll.Identity.Domain.Entities
{
	public class UserRole : BaseEntity<UserRoleId>
	{
		public string Name { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LastModifiedDate { get; set; }
	}
}