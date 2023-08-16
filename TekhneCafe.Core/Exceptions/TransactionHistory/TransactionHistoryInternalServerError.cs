namespace TekhneCafe.Core.Exceptions.TransactionHistory
{
    public class TransactionHistoryInternalServerError : InternalServerErrorException
    {
        public TransactionHistoryInternalServerError() : base("A server side error occured with TransactionHistory transaction!")
        {

        }

        public TransactionHistoryInternalServerError(string message) : base(message)
        {

        }
    }
}
