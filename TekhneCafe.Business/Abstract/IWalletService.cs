using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.DTOs.Cart;

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
