using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Customers;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<Result<GetCustomerByIdResponse>>, ICacheable
    {
        public Guid Id { get; protected set; }

        public bool BypassCache { get; protected set; }

        public string CacheKey { get; protected set; }

        public TimeSpan? SlidingExpiration { get; protected set; }

        public GetCustomerByIdQuery(Guid customerId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            Id = customerId;
            BypassCache = bypassCache;
            CacheKey = CacheKeys.Common.GetEntityByIdCacheKey<Guid, Customer>(customerId);
            SlidingExpiration = slidingExpiration;
        }
    }
}