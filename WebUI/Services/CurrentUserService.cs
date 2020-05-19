using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Users.Queries.GetAuthenticatedUser;
using Microsoft.AspNetCore.Http;
using Shared.Support.Constants;
using WebUI.Common;

namespace WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserAuthenticatedVm user = httpContextAccessor.HttpContext?.Session.GetObjectFromJson<UserAuthenticatedVm>("User");

            UserId = user?.Id ?? SystemConst.SYSTEM_USER_ID;
            UserName = user?.Name ?? SystemConst.SYSTEM_USER;
            UserProfileId = user?.UserProfileId ?? SystemConst.ADMIN_USER_PROFILE_ID;
            UserProfileEnum = user?.UserProfileEnum ?? UserProfileEnum.administrator;
        }

        public int UserId { get; }

        public string UserName { get; }

        public int UserProfileId { get; }

        public UserProfileEnum UserProfileEnum { get; }
    }
}
