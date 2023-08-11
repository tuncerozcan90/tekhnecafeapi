using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class ImageManager : IImageService
    {
        private readonly IImageDal _imageDal;

        public ImageManager(IImageDal imageDal)
        {
            _imageDal = imageDal;
        }

        public async Task<Entity.Concrete.Image> GetImageByIdAsync(Guid imageId)
        {
            return await _imageDal.GetByIdAsync(imageId);
        }

        public async Task CreateImageAsync(Entity.Concrete.Image image)
        {
            await _imageDal.AddAsync(image);
        }

        public async Task UpdateImageAsync(Entity.Concrete.Image image)
        {
            await _imageDal.UpdateAsync(image);
        }

        public async Task DeleteImageAsync(Guid imageId)
        {
            var image = await _imageDal.GetByIdAsync(imageId);
            if (image != null)
            {
                await _imageDal.HardDeleteAsync(image);
            }
        }

        public Task<List<Image>> GetAllImagesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

