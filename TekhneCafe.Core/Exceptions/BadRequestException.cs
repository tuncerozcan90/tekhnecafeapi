namespace TekhneCafe.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message = "Bad request exception!") : base(message)
        {

        }
    }
}
