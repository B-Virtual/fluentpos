using System.Net;
using FluentPOS.Shared.Core.Exceptions;

namespace FluentPOS.Modules.Accounting.Core.Exceptions
{
    public class AccountingException : CustomException
    {
        public AccountingException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message, statusCode: statusCode)
        {
        }
    }
}