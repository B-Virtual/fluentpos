using System.Threading.Tasks;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.Accounting.Core.Features.Accounting.Commands;
using FluentPOS.Modules.Accounting.Core.Interfaces;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.DTOs.People.Customers;

namespace FluentPOS.Modules.Accounting.Core.Factories
{
    public class AccountFactory : IAccountFactory
    {
        private readonly IEntityReferenceService _referenceService;

        public AccountFactory(IEntityReferenceService referenceService)
        {
            _referenceService = referenceService;
        }

        public async Task<Account> CreateAccount(GetCustomerByIdResponse customer)
        {
            var account = Account.InitializeAccount();
            string referenceNumber = await _referenceService.TrackAsync(account.GetType().Name);
            account.SetReferenceNumber(referenceNumber);

            account.AddCustomer(customer);

            return account;
        }

        public async Task<Payment> CreatePayment(RegisterPaymentCommand command)
        {
            var payment = new Payment(command.Amount, command.CustomerId);

            string referenceNumber = await _referenceService.TrackAsync(payment.GetType().Name);
            payment.SetReferenceNumber(referenceNumber);

            return payment;
        }
    }
}