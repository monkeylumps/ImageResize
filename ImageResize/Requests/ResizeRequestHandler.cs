using System.Threading;
using System.Threading.Tasks;
using ImageResize.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace ImageResize.Requests
{
    public class ResizeRequestHandler : IRequestHandler<ResizeRequest, byte[]>
    {
        private readonly IImageService _imageService;
        private readonly IDistributedCache _cache;

        public ResizeRequestHandler(IImageService imageService, IDistributedCache cache)
        {
            _imageService = imageService;
            _cache = cache;
        }

        public async Task<byte[]> Handle(ResizeRequest request, CancellationToken cancellationToken)
        {
            var key = $"{request.Resolution}/{request.BackgroundColour}/{request.Watermark}/{request.FileType}";

            var image = await _cache.GetAsync(key, cancellationToken);

            if (image != null) return image;

            image =  _imageService.MutateImage(request);
            await _cache.SetAsync(key, image, cancellationToken);

            return image;
        }
    }
}
