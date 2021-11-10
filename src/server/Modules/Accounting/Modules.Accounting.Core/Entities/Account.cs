using System;
using System.Collections.Generic;
using System.Linq;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.People.Customers;

namespace FluentPOS.Modules.Accounting.Core.Entities
{
    public class Account : BaseEntity
    {
        public string ReferenceNumber { get; private set; }

        public decimal Total { get; private set; }

        public Guid AccountHolderId { get; private set; }

        public string HolderName { get; private set; }

        public DateTime LastPayment { get; private set; }

        public virtual ICollection<Payment> Payments { get; private set; } = new List<Payment>();

        public DateTime Timestamp { get; private set; }

        public static Account InitializeAccount()
        {
            return new Account{ Timestamp = DateTime.Now };
        }

        public void AddCustomer(GetCustomerByIdResponse customer)
        {
            AccountHolderId = customer.Id;
            HolderName = customer.Name;
        }

        public void SetReferenceNumber(string referenceNumber)
        {
            ReferenceNumber = referenceNumber;
        }

        public void AddPayment(Payment payment)
        {
            Payments.Add(payment);
            RecalculateTotal();
        }

        internal void AddPayment(decimal amount)
        {
            Payments.Add(new Payment(amount, Id));
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            this.Total = Payments.Sum(x => x.Amount);
        }
    }
}