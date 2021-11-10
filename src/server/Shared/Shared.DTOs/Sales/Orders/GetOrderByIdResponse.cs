using System;
using System.Collections.Generic;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
       public record GetOrderByIdResponse
       (
              Guid Id,
              string ReferenceNumber,
              DateTime TimeStamp,
              Guid CustomerId,
              string CustomerName,
              string CustomerPhone,
              string CustomerEmail,
              decimal SubTotal,
              decimal Tax,
              decimal Discount,
              decimal Total,
              bool IsPaid,
              string Note,
              ICollection<ProductResponse> Products
       );

}