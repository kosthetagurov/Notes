namespace Notes.Extensions
{
    public class AccessRestrictionException : Exception
    {
        public AccessRestrictionException() : base("Illegal access attempt.")
        {
        }
    }
}
