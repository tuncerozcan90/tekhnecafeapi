namespace TekhneCafe.Core.Exceptions.Attribute
{
    public class AttributeBadRequestException : BadRequestException
    {
        public AttributeBadRequestException() : base("Attribute bad request!")
        {

        }

        public AttributeBadRequestException(string message) : base(message)
        {

        }
    }
}
