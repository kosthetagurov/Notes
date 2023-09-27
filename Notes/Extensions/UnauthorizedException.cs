namespace Notes.Extensions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("User cannot be recognized. Invalid user token.")
        {

        }
    }
}
