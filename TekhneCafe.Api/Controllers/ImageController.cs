using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Models;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            string filePath = await _imageService.UploadImage(request);
            return StatusCode(200, filePath);

        }

        //    private readonly IConfiguration _configuration;
        //    private readonly MinioClient _minioClient;

        //    public ImageController(IConfiguration configuration)
        //    {
        //        _configuration = configuration;

        //        _minioClient = new MinioClient()
        //            .WithEndpoint("127.0.0.1:9000")
        //            .WithCredentials("z0oSm6ZNjK1kgdekDzwk", "2VlllGrEu1lBl9upJvCiBbeZG5jWsNTB9kZ5GRs2")
        //            .Build();
        //    }

        //    [HttpPost("UploadImage")]
        //    public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        //    {
        //        MiniIORequestModel requestModel = new MiniIORequestModel
        //            (request.BucketName, request.Image.FileName, request.Image.OpenReadStream(), request.Image.ContentType, (int)request.Image.Length);
        //        var filePath = await fileUpload(_minioClient, requestModel);
        //        return StatusCode(200, filePath);

        //    }

        //    [HttpDelete("RemoveImage")]
        //    public async Task<IActionResult> RemoveImage([FromBody] RemoveImageRequest request)
        //    {
        //        try
        //        {
        //            var bucketName = request.BucketName;
        //            var objectName = request.ObjectName;

        //            var removeObjectArgs = new RemoveObjectArgs()
        //                .WithBucket(bucketName)
        //                .WithObject(objectName);

        //            await _minioClient.RemoveObjectAsync(removeObjectArgs);

        //            return StatusCode(200, "Başarılı");
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("Image Remove Error: {0}", e.Message);
        //            return StatusCode(500, "Bir hata oluştu");
        //        }
        //    }

        //    [HttpGet("GetImage")]
        //    public async Task<IActionResult> GetImage([FromQuery] GetImageRequest request)
        //    {
        //        try
        //        {
        //            var bucketName = request.BucketName;
        //            var objectName = request.ObjectName;

        //            GetObjectArgs getObjectArgs = new GetObjectArgs()
        //                                 .WithBucket(bucketName)
        //                                 .WithObject(objectName)
        //                                 .WithCallbackStream((stream) =>
        //                                 {
        //                                     stream.CopyTo(Console.OpenStandardOutput());
        //                                 });

        //            var presignedUrl = await _minioClient.GetObjectAsync(getObjectArgs);

        //            return Ok(presignedUrl);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("Image Get Error: {0}", e.Message);
        //            return StatusCode(500, "Bir hata oluştu");
        //        }
        //    }

        //    private async Task<string> fileUpload(MinioClient minio, MiniIORequestModel request)
        //    {

        //        var bucketName = request.BucketName;
        //        // var objectName = request.ObjectName;
        //        var stream = request.Stream;
        //        var contentType = request.ContentType;
        //        var length = request.Length;

        //        try
        //        {
        //            string randomImageName = Path.GetRandomFileName().Replace(".", "");
        //            var objectName = randomImageName + Path.GetExtension(request.ObjectName);

        //            // Make a bucket on the server, if not already present.
        //            var beArgs = new BucketExistsArgs()
        //                .WithBucket(bucketName);
        //            bool found = await minio.BucketExistsAsync(beArgs);
        //            if (!found)
        //            {
        //                var mbArgs = new MakeBucketArgs()
        //                    .WithBucket(bucketName);
        //                await minio.MakeBucketAsync(mbArgs);
        //            }

        //            var putObjectArgs = new PutObjectArgs()
        //                .WithBucket(bucketName)
        //                .WithObject(objectName)
        //                .WithStreamData(stream)
        //                .WithContentType(contentType)
        //                .WithObjectSize(length);
        //            await minio.PutObjectAsync(putObjectArgs);
        //            Console.WriteLine("Successfully uploaded " + objectName);
        //            return bucketName + "/" + objectName;

        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("File Upload Error: {0}", e.Message);
        //            throw;
        //        }
        //    }

        //}

        //public class GetImageRequest
        //{
        //    public string BucketName { get; set; }
        //    public string ObjectName { get; set; }
        //}


        //public class UploadImageRequest
        //{
        //    public IFormFile Image { get; set; }
        //    public string BucketName { get; set; }
        //}

        //public class RemoveImageRequest
        //{
        //    public string BucketName { get; set; }
        //    public string ObjectName { get; set; }
        //}
        //public class MiniIORequestModel
        //{
        //    public MiniIORequestModel(string bucketName, string objectName, Stream stream, string contentType, int length)
        //    {
        //        BucketName = bucketName;
        //        ObjectName = objectName;
        //        Stream = stream;
        //        ContentType = contentType;
        //        Length = length;
        //    }

        //    public string BucketName { get; set; }
        //    public string ObjectName { get; set; }
        //    public Stream Stream { get; set; }
        //    public string ContentType { get; set; }
        //    public int Length { get; set; }
        //}
    }
}