using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TekhneCafe.Business.Abstract
{
    public interface IImageService
    {
        Task<Entity.Concrete.Image> GetImageByIdAsync(Guid imageId);
        Task<List<Entity.Concrete.Image>> GetAllImagesAsync();
        Task CreateImageAsync(Entity.Concrete.Image image);
        Task UpdateImageAsync(Entity.Concrete.Image image);
        Task DeleteImageAsync(Guid imageId);
    }
}
