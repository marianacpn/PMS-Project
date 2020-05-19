using Application.Common.Enums;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }

        string UserName { get; }

        int UserProfileId { get; }

        UserProfileEnum UserProfileEnum { get; }
    }
}
