using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace FluentPOS.Modules.Accounting.Core.Features.Accounting.Events
{
    public class PaymentEventHandler :
        INotificationHandler<PaymentRegisteredEvent>
    {
        private readonly ILogger<PaymentEventHandler> _logger;
        private readonly IStringLocalizer<PaymentEventHandler> _localizer;

        public PaymentEventHandler(ILogger<PaymentEventHandler> logger, IStringLocalizer<PaymentEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public Task Handle(PaymentRegisteredEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(PaymentRegisteredEvent)} Raised."]);
            return Task.CompletedTask;
        }
    }
}