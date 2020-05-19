using Application.Common.Interfaces;

namespace Application.Common.Handlers
{
    public abstract class CommandBaseHandler
    {
        protected readonly IApplicationContext context;
        protected readonly ICurrentUserService currentUserService;

        public CommandBaseHandler(IApplicationContext context, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.currentUserService = currentUserService;
        }
    }
}
