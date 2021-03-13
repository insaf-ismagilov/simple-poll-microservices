using AutoMapper;
using SimplePoll.Identity.Application.Models.DataAccess;
using SimplePoll.Identity.Application.Models.Requests;
using SimplePoll.Identity.Application.Queries;
using SimplePoll.Identity.Domain.Entities;
using SimplePoll.Identity.Domain.Enums;

namespace SimplePoll.Identity.Application.Profiles
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