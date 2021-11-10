using System;

namespace FluentPOS.Shared.DTOs.Accounting.Account
{
    public record PaymentResponse(
        Guid Id,
        decimal Amount,
        DateTime Timestamp
    );
}