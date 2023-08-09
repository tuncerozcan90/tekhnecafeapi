using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class TranscationTypeManager : ITranscationTypeService
    {
        private readonly ITransactionTypeDal _transactionTypeDal;

        public TranscationTypeManager(ITransactionTypeDal transactionTypeDal)
        {
            _transactionTypeDal = transactionTypeDal;
        }

        public async Task CreateTranscationTypeAsync(TranscationType transcationType)
        {
            await _transactionTypeDal.AddAsync(transcationType);
        }

        public async Task DeleteTranscationTypeAsync(Guid transcationTypeId)
        {
           var transcationType = await _transactionTypeDal.GetByIdAsync(transcationTypeId);
            if (transcationType != null) { await _transactionTypeDal.HardDeleteAsync(transcationType); }
        }

        public async Task<List<TranscationType>> GetAllTranscationTypeAsync()
        {
           return await _transactionTypeDal.GetAll().ToListAsync();
        }

        public async Task<TranscationType> GetTranscationTypeByIdAsync(Guid transcationTypeId)
        {
            return await _transactionTypeDal.GetByIdAsync(transcationTypeId);
        }

        public async Task UpdateTranscationTypeAsync(TranscationType transcationType)
        {
          await _transactionTypeDal.UpdateAsync(transcationType);
        }
    }
}
