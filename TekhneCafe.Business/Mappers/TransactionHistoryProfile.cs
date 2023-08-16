using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.DTOs.Cart;
using TekhneCafe.Core.DTOs.Transaction;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Mappers
{
    public class TransactionHistoryProfile : Profile
    {
        public TransactionHistoryProfile()
        {
            CreateMap<TransactionHistoryAddDto, TransactionHistory>();
        }
    }
}
