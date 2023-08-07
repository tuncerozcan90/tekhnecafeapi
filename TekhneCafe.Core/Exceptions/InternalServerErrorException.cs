namespace TekhneCafe.Core.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message = "Internal server error exception!") : base(message)
        {

        }
    }
}
