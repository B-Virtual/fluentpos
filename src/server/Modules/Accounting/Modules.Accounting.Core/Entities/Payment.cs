using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Accounting.Core.Entities
{
    public class Payment : BaseEntity
    {
        public string ReferenceNumber { get; private set; }

        public decimal Amount { get; private set; }

        public DateTime Timestamp { get; private set; }

        public Guid AccountId { get; private set; }

        public virtual Account Account { get; private set; }

        public Payment(decimal amount, Guid accountId)
        {
            Amount = amount;
            AccountId = accountId;
            Timestamp = DateTime.Now;
        }

        public void SetReferenceNumber(string referenceNumber)
        {
            ReferenceNumber = referenceNumber;
        }
    }
}