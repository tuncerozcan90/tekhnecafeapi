namespace TekhneCafe.Business.Abstract
{
    public interface IWalletService
    {
        Task<float> GetWalletBalanceAsync(Guid userId);
        Task<bool> AddToWalletAsync(Guid userId, float amount);
        Task<bool> WithdrawFromWalletAsync(Guid userId, float amount);
        Task<float> GetTotalWalletBalanceAsync();
    }
}
