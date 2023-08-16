namespace TekhneCafe.Core.Exceptions.TransactionHistory
{
    public class TransactionHistoryAlreadyExistsException : BadRequestException
    {
        public TransactionHistoryAlreadyExistsException() : base("TransactionHistory already exists!")
        {

        }

        public TransactionHistoryAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
