namespace TekhneCafe.DataAccess.Helpers.Transaction
{
    public interface ITransactionManagement
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        void RollbackTransactionAsync();
    }
}
