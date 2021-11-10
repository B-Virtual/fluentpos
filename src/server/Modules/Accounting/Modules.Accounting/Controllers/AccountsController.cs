using System;
using System.Threading.Tasks;
using FluentPOS.Modules.Accounting.Core.Features.Accounting.Commands;
using FluentPOS.Modules.Accounting.Core.Features.Accounting.Queries;
using FluentPOS.Shared.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Accounting.Controllers
{
    [ApiVersion("1")]
    internal sealed class AccountsController : BaseController
    {
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Accounting.View)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new GetAccountByCustomerIdQuery { Id = id }));
        }

        [HttpPost("payments")]
        [Authorize(Policy = Permissions.Accounting.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterPaymentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}