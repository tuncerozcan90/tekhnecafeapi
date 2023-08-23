namespace TekhneCafe.Core.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message = "İşlem sırasında eklenmeyen bir hata oluştu!") : base(message)
        {

        }
    }
}
