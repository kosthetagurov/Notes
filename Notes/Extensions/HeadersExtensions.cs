using Microsoft.Extensions.Primitives;

namespace Notes.Extensions
{
    public static class HeadersExtensions
    {
        public static string GetUserToken(this IHeaderDictionary pairs)
        {
            pairs.TryGetValue("UserToken", out StringValues token);

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedException();
            }

            return token;
        }
    }
}
