namespace TekhneCafe.Core.Exceptions.TransactionHistory
{
    public class TransactionHistoryBadRequestException : BadRequestException
    {
        public TransactionHistoryBadRequestException() : base("TransactionHistory bad request!")
        {

        }

        public TransactionHistoryBadRequestException(string message) : base(message)
        {

        }
    }
}
