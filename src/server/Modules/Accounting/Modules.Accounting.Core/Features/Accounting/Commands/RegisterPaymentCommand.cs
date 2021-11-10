using System;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Accounting.Account;
using MediatR;

namespace FluentPOS.Modules.Accounting.Core.Features.Accounting.Commands
{
    public class RegisterPaymentCommand : IRequest<Result<GetAccountByCustomerIdResponse>>
    {
        public Guid CustomerId { get; set; }

        public decimal Amount { get; set; }
    }
}