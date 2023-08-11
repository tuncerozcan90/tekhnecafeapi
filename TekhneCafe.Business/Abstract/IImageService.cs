using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.DTOs.Image;
using TekhneCafe.Core.Filters.AppRole;
using TekhneCafe.Core.Filters.Image;
using static System.Net.Mime.MediaTypeNames;

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
