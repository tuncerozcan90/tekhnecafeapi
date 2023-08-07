namespace TekhneCafe.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message = "Not found exception!") : base(message)
        {

        }
    }
}
