using System;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Accounting.Account;
using MediatR;

namespace FluentPOS.Modules.Accounting.Core.Features.Accounting.Queries
{
    public class GetAccountByCustomerIdQuery : IRequest<Result<GetAccountByCustomerIdResponse>>
    {
        public Guid Id { get; set; }
    }
}