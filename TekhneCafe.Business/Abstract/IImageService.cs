using TekhneCafe.Core.DTOs.Image;
using TekhneCafe.Core.Filters.Image;

namespace TekhneCafe.Business.Abstract
{
    public interface IImageService
    {
        Task<ImageListDto> GetImageByIdAsync(string id);
        List<ImageListDto> GetAllImages(ImageRequestFilter filters = null);
        Task CreateImageAsync(ImageAddDto ımageAddDto);
        Task UpdateImageAsync(ImageUpdateDto ımageUpdateDto);
        Task DeleteImageAsync(string id);
    }
}
