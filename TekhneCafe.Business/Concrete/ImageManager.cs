

using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.Image;
using TekhneCafe.Core.Exceptions.Image;
using TekhneCafe.Core.Filters.Image;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class ImageManager : IImageService
    {
        private readonly IImageDal _imageDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public ImageManager(IImageDal imageDal, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _imageDal = imageDal;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        public async Task CreateImageAsync(ImageAddDto imageAddDto)
        {
            Image image = _mapper.Map<Image>(imageAddDto);
            await _imageDal.AddAsync(image);
        }

        public async Task DeleteImageAsync(string id)
        {
            Image image = await GetImageById(id);
            await _imageDal.SafeDeleteAsync(image);
        }

        public List<ImageListDto> GetAllImages(ImageRequestFilter filters = null)
        {
            var filteredResult = new ImageFilterService().FilterRoles(_imageDal.GetAll(), filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return _mapper.Map<List<ImageListDto>>(filteredResult.ResponseValue);
        }

        public async Task<ImageListDto> GetImageByIdAsync(string id)
        {
            var image = await GetImageById(id);
            return _mapper.Map<ImageListDto>(image);
        }

        public async Task UpdateImageAsync(ImageUpdateDto imageUpdateDto)
        {
            Image image = await GetImageById(imageUpdateDto.Id);
            _mapper.Map(imageUpdateDto, image);
            await _imageDal.UpdateAsync(image);
        }

        private async Task<Image> GetImageById(string id)
        {
            Image image = await _imageDal.GetByIdAsync(Guid.Parse(id));
            if (image is null)
                throw new ImageNotFoundException();

            return image;
        }
    }
}

