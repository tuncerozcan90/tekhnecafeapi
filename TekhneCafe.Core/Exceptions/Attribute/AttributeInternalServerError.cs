namespace TekhneCafe.Core.Exceptions.Attribute
{
    public class AttributeInternalServerError : InternalServerErrorException
    {
        public AttributeInternalServerError() : base("A server side error occured with Attribute transaction!")
        {

        }

        public AttributeInternalServerError(string message) : base(message)
        {

        }
    }
}
