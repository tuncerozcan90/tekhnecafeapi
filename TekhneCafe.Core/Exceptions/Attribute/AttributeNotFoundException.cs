namespace TekhneCafe.Core.Exceptions.Attribute
{
    public class AttributeNotFoundException : NotFoundException
    {
        public AttributeNotFoundException() : base("Attribute not found exception!")
        {

        }

        public AttributeNotFoundException(string message) : base(message)
        {

        }
    }
}
