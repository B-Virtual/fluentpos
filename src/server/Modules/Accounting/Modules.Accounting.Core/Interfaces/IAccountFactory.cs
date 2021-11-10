using System.Threading.Tasks;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.Accounting.Core.Features.Accounting.Commands;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.DTOs.People.Customers;

namespace FluentPOS.Modules.Accounting.Core.Interfaces
{
    public interface IAccountFactory : IEntityFactory
    {
        Task<Account> CreateAccount(GetCustomerByIdResponse customer);

        Task<Payment> CreatePayment(RegisterPaymentCommand command);
    }
}