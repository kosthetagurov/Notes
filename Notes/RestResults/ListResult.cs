using Newtonsoft.Json.Linq;

using Notes.Extensions;

using System.Net;

namespace Notes.RestResults
{
    public class ListResult<T> : RestResultBase<IEnumerable<T>>
        where T : class
    {
        public ListResult(HttpStatusCode statusCode, IEnumerable<T> result)
            : base(statusCode, result)
        {
        }

        public override string ToJson()
        {
            var jObj = new JObject
            {
                { "status", JToken.FromObject(StatusCode) },
                { "total", JToken.FromObject(Result.Count()) },
                { "data", Result.ToJsonString() }
            };

            return jObj.ToString();
        }
    }
}
