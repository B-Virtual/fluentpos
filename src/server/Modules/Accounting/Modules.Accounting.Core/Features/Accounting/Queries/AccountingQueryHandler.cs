using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using AutoMapper;
using FluentPOS.Modules.Accounting.Core.Abstractions;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.Accounting.Core.Exceptions;
using FluentPOS.Modules.Accounting.Core.Interfaces;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Accounting.Account;
using FluentPOS.Shared.DTOs.Accounting.Payments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Accounting.Core.Features.Accounting.Queries
{
    internal class AccountingQueryHandler :
                IRequestHandler<GetAccountByCustomerIdQuery, Result<GetAccountByCustomerIdResponse>>,
                IRequestHandler<GetPaymentsQuery, PaginatedResult<GetPaymentsResponse>>
    {
        private readonly IAccountingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAccountingDbContext _accountingDbContext;
        private readonly ICustomerService _customerService;
        private readonly IAccountFactory _accountFactory;
        private readonly IStringLocalizer<AccountingQueryHandler> _localizer;

        public AccountingQueryHandler(
            IAccountingDbContext context,
            IMapper mapper,
            IAccountingDbContext accountingDbContext,
            ICustomerService customerService,
            IAccountFactory accountFactory,
            IStringLocalizer<AccountingQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _accountingDbContext = accountingDbContext;
            _customerService = customerService;
            _accountFactory = accountFactory;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetPaymentsResponse>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Payment, GetPaymentsResponse>> expression = e => new GetPaymentsResponse(e.Id, e.Amount, e.Timestamp, e.AccountId, e.Account.HolderName);

            var queryable = _context.Payments.AsNoTracking().AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            // string ordering = new OrderByConverter().Convert(request.OrderBy);
            // queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.TimeStamp);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.AccountId.ToString().ToLower(), $"%{request.SearchString.ToLower()}%")
                                                 || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%")
                                                 || EF.Functions.Like(x.Amount.ToString().ToLower(), $"%{request.SearchString.ToLower()}%")
                                                 || EF.Functions.Like(x.Account.HolderName.ToLower(), $"%{request.SearchString.ToLower()}%"));
            }

            var resultList = await queryable
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (resultList == null)
            {
                throw new AccountingException(_localizer["Payments Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetPaymentsResponse>>(resultList);
        }

        public async Task<Result<GetAccountByCustomerIdResponse>> Handle(GetAccountByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.AsNoTracking()
                .Include(x => x.Payments)
                .OrderBy(x => x.Timestamp)
                .SingleOrDefaultAsync(x => x.AccountHolderId == request.Id, cancellationToken: cancellationToken);

            if (account == null)
            {
                var customer = await _customerService.GetCustomerAsync(request.Id);

                if (!customer.Succeeded || customer.Data == null)
                {
                    throw new AccountingException(_localizer["Customer not found!"], HttpStatusCode.NotFound);
                }

                account = await _accountFactory.CreateAccount(customer.Data);
                await _accountingDbContext.Accounts.AddAsync(account, cancellationToken);
            }

            var mappedData = _mapper.Map<Account, GetAccountByCustomerIdResponse>(account);

            return await Result<GetAccountByCustomerIdResponse>.SuccessAsync(data: mappedData);

        }
    }
}