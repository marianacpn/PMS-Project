using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class RequestExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IApplicationContext context;

        public RequestExceptionBehaviour(ICurrentUserService currentUserService, IApplicationContext context)
        {
            this.currentUserService = currentUserService;
            this.context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (FluentValidation.ValidationException ex)
            {
                throw ex;
            }
            catch (WarningException ex)
            {
                ApplicationLog applicationLog = new ApplicationLog(ex, Shared.Support.Enums.ApplicationLogTypesEnum.Warning, typeof(TRequest).Name, currentUserService.UserId, currentUserService.UserName);

                context.ApplicationLogs.Add(applicationLog);

                await context.SaveChangesAsync(cancellationToken);

                throw ex;
            }
            catch (NotLoggableException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ApplicationLog applicationLog = new ApplicationLog(ex, Shared.Support.Enums.ApplicationLogTypesEnum.Error, typeof(TRequest).Name, currentUserService.UserId, currentUserService.UserName);

                context.ApplicationLogs.Add(applicationLog);

                await context.SaveChangesAsync(cancellationToken);

                //throw new NotLoggableException(ex.Message, ex);
                throw ex;
            }
        }
    }
}
