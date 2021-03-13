using AutoMapper;
using SimplePoll.Identity.Entities;
using SimplePoll.Identity.Enums;
using SimplePoll.Identity.Models.DataAccess;
using SimplePoll.Identity.Models.Requests;
using SimplePoll.Identity.Queries;

namespace SimplePoll.Identity.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<SignInRequest, GetUserByEmailQuery>();
			
			CreateMap<UserRecord, User>()
				.ForMember(m => m.Role,
					o => o.MapFrom(m => new UserRole
					{
						Id = (UserRoleId) m.RoleId,
						Name = m.RoleName,
						CreatedDate = m.RoleCreatedDate,
						LastModifiedDate = m.RoleLastModifiedDate
					}));
		}
	}
}