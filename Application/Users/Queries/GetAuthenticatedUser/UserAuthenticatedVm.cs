using Application.Common.Enums;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Users.Queries.GetAuthenticatedUser
{
    public class UserAuthenticatedVm : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int UserProfileId { get; set; }
        public UserProfileEnum UserProfileEnum { get; set; }
        public string UserProfileName { get; set; }
        public int NotificationQuantity { get; set; }

        public void Mapping(Profile profile)
        {
            //profile.CreateMap<User, UserAuthenticatedVm>()
            //    .ForMember(dest => dest.UserProfileName, opt => opt.MapFrom(src => src.UserProfile.Name))
            //    .ForMember(dest => dest.UserProfileEnum, opt => opt.MapFrom(src => (UserProfileEnum)src.UserProfileId));
        }
    }
}