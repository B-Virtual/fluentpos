using System;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public record ProductResponse
    (
        Guid OrderId,
        Guid ProductId,
        int Quantity,
        string Name,
        string Category,
        string Brand,
        decimal Price,
        decimal Tax,
        decimal Discount,
        decimal Total
    );
}