using System.ComponentModel;
using System.IO;
using System.Reflection;
using ImageResize.Enums;
using ImageResize.Requests;
using Microsoft.OpenApi.Extensions;
using SixLabors.ImageSharp;

namespace ImageResize.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageProcessService _imageProcessService;

        public ImageService(IImageProcessService imageProcessService)
        {
            _imageProcessService = imageProcessService;
        }

        public byte[] MutateImage(ResizeRequest request)
        {
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = location + @"\Images\01_04_2019_001103.png";

            var fileStream = File.OpenRead(filePath);

            using var outputStream = new MemoryStream();

            using (var image = Image.Load(fileStream))
            {
                var resolution = request.Resolution.GetAttributeOfType<DescriptionAttribute>().Description;
                _imageProcessService.ResizeImage(image, resolution);

                if (!string.IsNullOrWhiteSpace(request.Watermark))
                {
                    _imageProcessService.AddWaterMark(image, request.Watermark);
                }

                if (request.BackgroundColour != BackgroundColour.None)
                {
                    var colourHex = request.BackgroundColour.GetAttributeOfType<DescriptionAttribute>().Description;
                    _imageProcessService.ChangeBackgroundColour(image, colourHex);
                }

                switch (request.FileType)
                {
                    case FileType.Png:
                        image.SaveAsPng(outputStream);
                        break;
                    case FileType.Jpg:
                        image.SaveAsJpeg(outputStream);
                        break;
                }
            }

            return outputStream.ToArray();
        }
    }
}
