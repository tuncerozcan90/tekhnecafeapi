﻿using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Payment;
using TekhneCafe.Core.Exceptions;
using TekhneCafe.Core.Extensions;
using TekhneCafe.DataAccess.Helpers.Transaction;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private readonly IWalletService _walletService;
        private readonly ITransactionManagement _transactionManagement;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly INotificationService _notificationService;
        private readonly HttpContext _httpContext;

        public PaymentManager(IWalletService walletService, ITransactionManagement transactionManagement, ITransactionHistoryService transactionHistoryService,
            IHttpContextAccessor httpContextAccessor, INotificationService notificationService)
        {
            _walletService = walletService;
            _transactionManagement = transactionManagement;
            _transactionHistoryService = transactionHistoryService;
            _notificationService = notificationService;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task Pay(PaymentDto paymentDto)
        {
            using (var transaction = await _transactionManagement.BeginTransactionAsync())
            {
                paymentDto.Description = paymentDto.Description == null ? "Ödeme yapıldı" : paymentDto.Description;
                try
                {
                    await _walletService.AddToWalletAsync(Guid.Parse(paymentDto.UserId), paymentDto.Amount);
                    await _transactionHistoryService.CreateTransactionHistoryAsync(paymentDto.Amount, TransactionType.Payment, paymentDto.Description, Guid.Parse(_httpContext.User.ActiveUserId()));
                    await _notificationService.CreateNotificationAsync("Ödemeniz başarıyla alınmıştır. Bu yazıya tıklayarak ödeme yaptığınız tutarı onaylayınız.", _httpContext.User.ActiveUserId(), false);
                    await transaction.CommitAsync();
                }
                catch
                {
                    throw new InternalServerErrorException();
                }
            }
        }
    }
}
