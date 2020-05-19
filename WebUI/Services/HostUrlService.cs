using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebUI.Services
{
    public class HostUrlService : IHostUrlService
    {
        public HostUrlService(IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext.Request;

            Url = request.Scheme + "://" + request.Host.Value;
        }

        public string Url { get; }
    }
}
