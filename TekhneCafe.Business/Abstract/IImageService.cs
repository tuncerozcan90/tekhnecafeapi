using Microsoft.AspNetCore.Mvc;
using Minio.DataModel;
using TekhneCafe.Business.Models;

namespace TekhneCafe.Business.Abstract
{
    public interface IImageService
    {
        Task<string> UploadImage([FromForm] UploadImageRequest request);
        Task RemoveImage([FromBody] RemoveImageRequest request);
        Task<ObjectStat> GetImage([FromQuery] GetImageRequest request);
    }
}
