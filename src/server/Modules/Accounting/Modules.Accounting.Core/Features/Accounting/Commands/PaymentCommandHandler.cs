using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentPOS.Modules.Accounting.Core.Abstractions;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.Accounting.Core.Features.Accounting.Events;
using FluentPOS.Modules.Accounting.Core.Features.Accounting.Queries;
using FluentPOS.Modules.Accounting.Core.Interfaces;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Accounting.Account;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Accounting.Core.Features.Accounting.Commands
{
    internal sealed class PaymentCommandHandler :
        IRequestHandler<RegisterPaymentCommand, Result<GetAccountByCustomerIdResponse>>
    {
        private readonly IEntityReferenceService _referenceService;
        private readonly ICustomerService _customerService;
        private readonly IAccountFactory _accountFactory;
        private readonly IMediator _mediator;
        private readonly IAccountingDbContext _accountingDbContext;
        private readonly IStringLocalizer<PaymentCommandHandler> _localizer;

        public PaymentCommandHandler(
            IStringLocalizer<PaymentCommandHandler> localizer,
            IAccountingDbContext accountingDbContext,
            IEntityReferenceService referenceService,
            ICustomerService customerService,
            IAccountFactory accountFactory,
            IMediator _mediator)
        {
            _localizer = localizer;
            _accountingDbContext = accountingDbContext;
            _referenceService = referenceService;
            _customerService = customerService;
            _accountFactory = accountFactory;
            this._mediator = _mediator;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<GetAccountByCustomerIdResponse>> Handle(RegisterPaymentCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var customerData = await _customerService.GetCustomerAsync(command.CustomerId);

            // Do all mandatory null checks
            if (customerData?.Data == null) throw new Exception("Unknown customer!");
            var customer = customerData.Data;

            // Check if an account has been created for the customer
            var account = _accountingDbContext.Accounts.Include(i => i.Payments).FirstOrDefault(x => x.AccountHolderId == customer.Id);

            // If not create an account for the customer
            if (account == default(Account))
            {
                account = await _accountFactory.CreateAccount(customer);
                await _accountingDbContext.Accounts.AddAsync(account, cancellationToken);
            }

            var payment = await _accountFactory.CreatePayment(command);
            await _accountingDbContext.Payments.AddAsync(payment, cancellationToken);

            account.AddPayment(payment);
            payment.AddDomainEvent(new PaymentRegisteredEvent(account, payment));

            await _accountingDbContext.SaveChangesAsync(cancellationToken);

            var result = await _mediator.Send(new GetAccountByCustomerIdQuery { Id = command.CustomerId }, cancellationToken);
            result.Messages.Add(string.Format(_localizer["Payment {0} added"], payment.ReferenceNumber));

            return result;
        }
    }
}