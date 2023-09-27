using Newtonsoft.Json.Linq;
using Notes.Extensions;

using System.Net;

namespace Notes.RestResults
{
    public class ErrorResult : RestResultBase<Exception>
    {
        public ErrorResult(Exception result) 
            : base(HttpStatusCode.InternalServerError, result)
        {
        }

        public ErrorResult(HttpStatusCode statusCode, Exception result)
            : base(statusCode, result)
        {
        }

        public override string ToJson()
        {
            var jObj = new JObject
            {
                { "status", JToken.FromObject(StatusCode) },
                { "data", Result.ToJsonString() }
            };

            return jObj.ToString();
        }
    }
}
