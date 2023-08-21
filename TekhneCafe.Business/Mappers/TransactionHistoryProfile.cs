using AutoMapper;
using TekhneCafe.Core.DTOs.Transaction;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Mappers
{
    public class TransactionHistoryProfile : Profile
    {
        public TransactionHistoryProfile()
        {
            CreateMap<TransactionHistory, TransactionHistoryListDto>();
        }
    }
}
