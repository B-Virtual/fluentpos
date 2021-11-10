using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Features.Customers.Queries;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Customers;
using MediatR;

namespace FluentPOS.Modules.People.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator _mediator;

        public CustomerService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<GetCustomerByIdResponse>> GetCustomerAsync(Guid customerId)
        {
            return await _mediator.Send(new GetCustomerByIdQuery(customerId));
        }
    }
}