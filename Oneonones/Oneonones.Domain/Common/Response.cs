using System.Net;

namespace Oneonones.Domain.Common
{
    public class Response<T>
    {
        public bool Succeeded { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public T Data { get; private set; }
        public IList<string> Errors { get; private set; }

        private Response(bool succeeded, HttpStatusCode statusCode, T data, IList<string> errors)
        {
            Succeeded = succeeded;
            StatusCode = statusCode;
            Data = data;
            Errors = errors;
        }

        public static Response<T> Success(HttpStatusCode statusCode, T data)
        {
            var response = new Response<T>(true, statusCode, data, null);
            return response;
        }

        public static Response<T> Error(HttpStatusCode statusCode, IList<string> errors)
        {
            var response = new Response<T>(false, statusCode, default, errors);
            return response;
        }

        public static Response<T> Error(HttpStatusCode statusCode, string error)
        {
            var response = Response<T>.Error(statusCode, new string[] { error });
            return response;
        }
    }
}