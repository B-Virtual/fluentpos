using System;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Accounting.Core.Features.Accounting.Events
{
    public class PaymentRegisteredEvent : Event
    {
        public Guid Id { get; }

        public Guid CustomerId { get; }

        public new DateTime Timestamp { get; }

        public PaymentRegisteredEvent(Account account, Payment payment)
        {
            CustomerId = account.AccountHolderId;
            Timestamp = payment.Timestamp;
            Id = payment.Id;
            AggregateId = payment.Id;
            RelatedEntities = new[] { typeof(Payment) };
        }
    }
}