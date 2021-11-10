using System;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Accounting.Account;

namespace FluentPOS.Shared.Core.IntegrationServices.Accounting
{
    public interface IAccountingService
    {
        Task<Result<GetAccountByCustomerIdResponse>> GetAccountDetailsByCustomerId(Guid cartId);
    }
}