using Newtonsoft.Json.Linq;

using System.Collections;
using System.Net;

namespace Notes.RestResults
{
    public abstract class RestResultBase<T> where T : class
    {
        public RestResultBase(HttpStatusCode statusCode, T result)
        {
            StatusCode = statusCode;
            Result = result;
        }

        public HttpStatusCode StatusCode { get; }
        public T Result { get; }

        public abstract string ToJson();
    }
}
