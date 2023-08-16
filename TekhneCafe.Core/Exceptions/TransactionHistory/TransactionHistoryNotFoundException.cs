namespace TekhneCafe.Core.Exceptions.TransactionHistory
{
    public class TransactionHistoryNotFoundException : NotFoundException
    {
        public TransactionHistoryNotFoundException() : base("TransactionHistory not found exception!")
        {

        }

        public TransactionHistoryNotFoundException(string message) : base(message)
        {

        }
    }
}
