using AutoMapper;
using SimplePoll.Identity.Domain.Entities;
using SimplePoll.Identity.Domain.Enums;
using SimplePoll.Identity.Infrastructure.Database.Models;

namespace SimplePoll.Identity.Infrastructure.Database.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRecord, User>()
                .ForMember(m => m.Role,
                    o => o.MapFrom(m => new UserRole
                    {
                        Id = (UserRoleId)m.RoleId,
                        Name = m.RoleName,
                        CreatedDate = m.RoleCreatedDate,
                        LastModifiedDate = m.RoleLastModifiedDate
                    }));
        }
    }
}