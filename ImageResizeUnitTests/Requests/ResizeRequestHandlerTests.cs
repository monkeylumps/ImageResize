using System;
using System.Threading;
using System.Threading.Tasks;
using ImageResize.Enums;
using ImageResize.Requests;
using ImageResize.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Xunit;

namespace ImageResizeUnitTests.Requests
{
    public class ResizeRequestHandlerTests
    {
        private readonly IRequestHandler<ResizeRequest, byte[]> _sut;
        private readonly Mock<IImageService> _mockFileService;
        private readonly Mock<IDistributedCache> _cache;
        private readonly byte[] _thing;

        public ResizeRequestHandlerTests()
        {
            _thing = new byte[1];

            _mockFileService = new Mock<IImageService>();
            _cache = new Mock<IDistributedCache>();
            _sut = new ResizeRequestHandler(_mockFileService.Object, _cache.Object);

        }

        [Fact]
        public async void GivenNoFileCachedWhenRequestedThenProcessedImage()
        {
            // Arrange
            var request = new ResizeRequest(Resolution.X1080, BackgroundColour.None, String.Empty, FileType.Jpg);

            _cache.Setup(cache => cache.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<byte[]>(null));

            // Act

            await _sut.Handle(request, CancellationToken.None);

            // Assert
            _mockFileService.Verify(service => service.MutateImage(It.IsAny<ResizeRequest>()), Times.AtLeastOnce);
        }

        [Fact]
        public async void GivenFileCachedWhenRequestedThenReturnCachedImage()
        {
            // Arrange
            var request = new ResizeRequest(Resolution.X1080, BackgroundColour.None, String.Empty, FileType.Jpg);

            _cache.Setup(cache => cache.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_thing));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            _mockFileService.Verify(service => service.MutateImage(It.IsAny<ResizeRequest>()), Times.Never);
        }
    }
}
