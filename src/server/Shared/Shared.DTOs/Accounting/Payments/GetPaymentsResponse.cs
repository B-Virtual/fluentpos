using System;

namespace FluentPOS.Shared.DTOs.Accounting.Payments
{
    public record GetPaymentsResponse(Guid Id, decimal Amount, DateTime TimeStamp, Guid AccountId, string CustomerName);
}