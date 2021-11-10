using System;
using System.Collections.Generic;

namespace FluentPOS.Shared.DTOs.Accounting.Account
{
    public record GetAccountByCustomerIdResponse(
        Guid Id,
        string HolderName,
        decimal Total,
        DateTime LastPayment,
        ICollection<PaymentResponse> Payments
        );
}