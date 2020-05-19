using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Common.Handlers
{
    public abstract class QueryBaseHandler
    {
        protected readonly IApplicationContext context;
        protected readonly IMapper mapper;

        public QueryBaseHandler(IApplicationContext  context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
    }
}
