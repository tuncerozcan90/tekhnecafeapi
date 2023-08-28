﻿using Microsoft.AspNetCore.Mvc;
using Minio;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MinioClient _minioClient;

        public ImageController(IConfiguration configuration)
        {
            _configuration = configuration;

            _minioClient = new MinioClient()
                .WithEndpoint("127.0.0.1:9000")
                .WithCredentials("AaNNRM1xfX31ZTjs8hIw", "7MG8Uwr3Nv60bYb4mWi8HfWlgSWDtjlFJEyrSNUs")
                .Build();
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            MiniIORequestModel requestModel = new MiniIORequestModel
                (request.BucketName, request.Image.Name, request.Image.OpenReadStream(), request.Image.ContentType, (int)request.Image.Length);
            await fileUpload(_minioClient, requestModel);
            return StatusCode(200, "Başarılı");

        }

        [HttpDelete("RemoveImage")]
        public async Task<IActionResult> RemoveImage([FromBody] RemoveImageRequest request)
        {
            try
            {
                var bucketName = request.BucketName;
                var objectName = request.ObjectName;

                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName);

                await _minioClient.RemoveObjectAsync(removeObjectArgs);

                return StatusCode(200, "Başarılı");
            }
            catch (Exception e)
            {
                Console.WriteLine("Image Remove Error: {0}", e.Message);
                return StatusCode(500, "Bir hata oluştu");
            }
        }

        public class UploadImageRequest
        {
            public IFormFile Image { get; set; }
            public string BucketName { get; set; }
        }

        public class RemoveImageRequest
        {
            public string BucketName { get; set; }
            public string ObjectName { get; set; }
        }

        private async Task fileUpload(MinioClient minio, MiniIORequestModel request)
        {

            var bucketName = request.BucketName;
            var objectName = request.ObjectName;
            var stream = request.Stream;
            var contentType = request.ContentType;
            var length = request.Length;

            try
            {
                // Make a bucket on the server, if not already present.
                var beArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);
                bool found = await minio.BucketExistsAsync(beArgs);
                if (!found)
                {
                    var mbArgs = new MakeBucketArgs()
                        .WithBucket(bucketName);
                    await minio.MakeBucketAsync(mbArgs);
                }
                // Upload a file to bucket.
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithStreamData(stream)
                    .WithContentType(contentType)
                    .WithObjectSize(length);
                await minio.PutObjectAsync(putObjectArgs);
                Console.WriteLine("Successfully uploaded " + objectName);
            }
            catch (Exception e)
            {

                Console.WriteLine("File Upload Error: {0}", e.Message);
                throw;
            }
        }

    }
    public class MiniIORequestModel
    {
        public MiniIORequestModel(string bucketName, string objectName, Stream stream, string contentType, int length)
        {
            BucketName = bucketName;
            ObjectName = objectName;
            Stream = stream;
            ContentType = contentType;
            Length = length;
        }

        public string BucketName { get; set; }
        public string ObjectName { get; set; }
        public Stream Stream { get; set; }
        public string ContentType { get; set; }
        public int Length { get; set; }
    }
}
