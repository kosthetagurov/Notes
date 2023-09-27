using Newtonsoft.Json.Linq;
using Notes.Extensions;

using System.Net;

namespace Notes.RestResults
{
    public class SimpleResult<T> : RestResultBase<T>
        where T : class
    {
        public SimpleResult(HttpStatusCode statusCode, T result) 
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
