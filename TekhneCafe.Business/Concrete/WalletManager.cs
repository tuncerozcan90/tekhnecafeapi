﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class WalletManager : IWalletService
    {
        private readonly IOrderDal _orderDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAppUserService _userService;

        public WalletManager(IAppUserService userService)
        {
            _orderDal = orderDal;
            _mapper = mapper;
            _httpContext = httpContext;
            _userService = userService;
        }

        public async Task<float> GetWalletBalanceAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId.ToString());
            return user.Wallet;
        }

        public async Task<bool> AddToWalletAsync(Guid userId, float amount)
        {
            var user = await _userService.GetUserByIdAsync(userId.ToString());
            user.Wallet += amount;
            await UpdateWalletAsync(user);
            return true;
        }

        public async Task<bool> WithdrawFromWalletAsync(Guid userId, float amount)
        {
            var user = await _userService.GetUserByIdAsync(userId.ToString());
            user.Wallet -= amount;
            await UpdateWalletAsync(user);
            return true;
        }

        public async Task<float> GetTotalWalletBalanceAsync()
        {
            var users = _userService.GetUserList();
            float totalBalance = users.Sum(user => user.Wallet);
            return totalBalance;
        }

        private async Task UpdateWalletAsync(AppUser user)
            => await _userService.UpdateUserAsync(user);
    }
}
