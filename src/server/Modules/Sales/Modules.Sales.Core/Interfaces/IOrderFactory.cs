using System.Threading.Tasks;
using FluentPOS.Modules.Sales.Core.Entities;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.DTOs.People.Customers;

namespace FluentPOS.Modules.Sales.Core.Interfaces
{
    public interface IOrderFactory : IEntityFactory
    {
        Task<Order> CreateOrder(GetCustomerByIdResponse customer);
    }
}
