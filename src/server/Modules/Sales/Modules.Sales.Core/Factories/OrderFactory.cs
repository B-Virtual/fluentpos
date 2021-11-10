using System.Threading.Tasks;
using FluentPOS.Modules.Sales.Core.Entities;
using FluentPOS.Modules.Sales.Core.Interfaces;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.DTOs.People.Customers;

namespace FluentPOS.Modules.Sales.Core.Factories
{
    public class OrderFactory : IOrderFactory
    {
        private readonly IEntityReferenceService _referenceService;

        public OrderFactory(IEntityReferenceService referenceService)
        {
            _referenceService = referenceService;
        }

        public async Task<Order> CreateOrder(GetCustomerByIdResponse customer)
        {
            var order = Order.InitializeOrder();
            string referenceNumber = await _referenceService.TrackAsync(order.GetType().Name);
            order.SetReferenceNumber(referenceNumber);

            order.AddCustomer(customer);

            return order;
        }
    }
}