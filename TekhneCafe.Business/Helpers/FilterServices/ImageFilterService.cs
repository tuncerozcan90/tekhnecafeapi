using ECommerce.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.Filters.AppRole;
using TekhneCafe.Core.Filters.Image;
using TekhneCafe.Core.ResponseHeaders;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Helpers.FilterServices
{
    public class ImageFilterService
    {
        public ImageResponseFilter<List<Image>> FilterRoles(IQueryable<Image> images, ImageRequestFilter filters)
        {
            var filteredImages = images.Skip(filters.Page * filters.Size).Take(filters.Size).ToList();
            Metadata metadata = new()
            {
                CurrentPage = filters.Page,
                PageSize = filters.Size,
                TotalEntities = images.Count(),
                TotalPages = images.Count() / filters.Size + 1,
            };
            var header = new CustomHeaders().AddPaginationHeader(metadata);

            return new()
            {
                ResponseValue = filteredImages,
                Headers = header
            };
        }
    }
}
