namespace TekhneCafe.Core.Exceptions.Attribute
{
    public class AttributeAlreadyExistsException : BadRequestException
    {
        public AttributeAlreadyExistsException() : base("Attribute already exists!")
        {

        }

        public AttributeAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
