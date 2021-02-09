using System;
using System.Globalization;
using System.Net;

namespace Oneonones.Service.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public ApiException(HttpStatusCode statusCode) : base()
        {
            StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
            StatusCode = statusCode;
        }
    }
}