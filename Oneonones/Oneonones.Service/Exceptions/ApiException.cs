using System.Net;
using Oneonones.Domain.Common;

namespace Oneonones.Service.Exceptions
{
    public class ApiException : Exception
    {
        public Error Error { get; set; }
        public HttpStatusCode StatusCode { get; private set; }

        public ApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            Error = new Error(message);
            StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, IList<string> list) : base(string.Join('\n', list))
        {
            Error = new Error(list);
            StatusCode = statusCode;
        }
    }
}