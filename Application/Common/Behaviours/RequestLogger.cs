using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class RequestLogger<IRequest> : IRequestPreProcessor<IRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public RequestLogger(ILogger<IRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Process(IRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(IRequest).Name;

            _logger.LogInformation("Solution CQRS Request:{Name}  UserId:{@UserId}  UserName{@UserName} Request:{@Request}",
                name, _currentUserService.UserId, _currentUserService.UserName, request);

            return Task.CompletedTask;
        }
    }
}
