using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Accounting.Payments;
using MediatR;

namespace FluentPOS.Modules.Accounting.Core.Features.Accounting.Queries
{
    public class GetPaymentsQuery : IRequest<PaginatedResult<GetPaymentsResponse>>
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string[] OrderBy { get; private set; }

        public string SearchString { get; private set; }
    }
}