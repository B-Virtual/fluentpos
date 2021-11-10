using System;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Customers;

namespace FluentPOS.Shared.Core.IntegrationServices.People
{
    public interface ICustomerService
    {
        Task<Result<GetCustomerByIdResponse>> GetCustomerAsync(Guid customerId);
    }
}