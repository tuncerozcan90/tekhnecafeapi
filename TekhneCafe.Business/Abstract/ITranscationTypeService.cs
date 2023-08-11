using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface ITranscationTypeService
    {
        Task<TranscationType> GetTranscationTypeByIdAsync(Guid transcationTypeId);
        Task<List<TranscationType>> GetAllTranscationTypeAsync();
        Task CreateTranscationTypeAsync(TranscationType transcationType);
        Task UpdateTranscationTypeAsync(TranscationType transcationType);
        Task DeleteTranscationTypeAsync(Guid transcationTypeId);
    }
}
