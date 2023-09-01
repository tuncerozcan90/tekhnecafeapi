using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Models;
using TekhneCafe.Core.Exceptions.Image;

namespace TekhneCafe.Business.Concrete
{
    public class ImageManager : IImageService
    {
        private readonly MinioClient _minioClient;

        public ImageManager(IConfiguration configuration)
        {
            _minioClient = new MinioClient()
                .WithEndpoint(configuration.GetValue<string>("Minio:Endpoint"))
                .WithCredentials(configuration.GetValue<string>("Minio:AccessKey"), configuration.GetValue<string>("Minio:SecretKey"))
                .Build();
        }

        public async Task<ObjectStat> GetImage(GetImageRequest request)
        {
            try
            {
                var bucketName = request.BucketName;
                var objectName = request.ObjectName;
                GetObjectArgs getObjectArgs = new GetObjectArgs()
                                     .WithBucket(bucketName)
                                     .WithObject(objectName)
                                     .WithCallbackStream((stream) =>
                                     {
                                         stream.CopyTo(Console.OpenStandardOutput());
                                     });

                var presignedUrl = await _minioClient.GetObjectAsync(getObjectArgs);
                return presignedUrl;
            }
            catch (Exception e)
            {
                throw new ImageInternalServerError();
            }
        }

        public async Task RemoveImage(RemoveImageRequest request)
        {
            try
            {
                var bucketName = request.BucketName;
                var objectName = request.ObjectName;
                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName);
                await _minioClient.RemoveObjectAsync(removeObjectArgs);
            }
            catch
            {
                throw new ImageInternalServerError();
            }
        }

        public async Task<string> UploadImage(UploadImageRequest request)
        {
            MiniIORequestModel requestModel = new MiniIORequestModel
                (request.BucketName, request.Image.FileName, request.Image.OpenReadStream(), request.Image.ContentType, (int)request.Image.Length);
            var filePath = await fileUpload(_minioClient, requestModel);
            return filePath;
        }

        private async Task<string> fileUpload(MinioClient minio, MiniIORequestModel request)
        {
            var bucketName = request.BucketName;
            var stream = request.Stream;
            var contentType = request.ContentType;
            var length = request.Length;
            try
            {
                string randomImageName = Path.GetRandomFileName().Replace(".", "");
                var objectName = randomImageName + Path.GetExtension(request.ObjectName);
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
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithStreamData(stream)
                    .WithContentType(contentType)
                    .WithObjectSize(length);
                await minio.PutObjectAsync(putObjectArgs);
                return bucketName + "/" + objectName;
            }
            catch
            {
                throw new ImageInternalServerError();
            }
        }
    }
}

