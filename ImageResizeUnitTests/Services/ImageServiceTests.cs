using System;
using System.Linq;
using ImageResize.Enums;
using ImageResize.Requests;
using ImageResize.Services;
using Moq;
using SixLabors.ImageSharp;
using Xunit;

namespace ImageResizeUnitTests.Services
{
    public class ImageServiceTests
    {
        private readonly IImageService _sut;
        private readonly Mock<IImageProcessService> _mockImageProcessService;

        public ImageServiceTests()
        {
            _mockImageProcessService = new Mock<IImageProcessService>();
            _sut = new ImageService(_mockImageProcessService.Object);
        }

        // resize

        // 1080
        // 20160
        [Fact]
        public void GivenResolution1080WhenImageResizedThenImageResized()
        {
            // Arrange
            var request = new ResizeRequest(Resolution.X1080, BackgroundColour.None, string.Empty, FileType.Png);

            // Act
            _sut.MutateImage(request);

            // Assert
            _mockImageProcessService.Verify(service => service.ResizeImage(It.IsAny<Image>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GivenResolution2160WhenImageResizedThenImageResized()
        {
            // Arrange
            var request = new ResizeRequest(Resolution.X2160, BackgroundColour.None, string.Empty, FileType.Png);

            // Act
            _sut.MutateImage(request);

            // Assert
            _mockImageProcessService.Verify(service => service.ResizeImage(It.IsAny<Image>(), It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public void GivenABackgroundColourWhenImageResizedThenBackgroundColourApplied()
        {
            // Arrange
            var request = new ResizeRequest(Resolution.X1080, BackgroundColour.Black, string.Empty, FileType.Png);

            // Act
            _sut.MutateImage(request);

            // Assert
            _mockImageProcessService.Verify(service => service.ChangeBackgroundColour(It.IsAny<Image>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GivenNoBackgroundColourWhenImageResizedThenNoBackgroundColourApplied()
        {
            // Arrange
            var request = new ResizeRequest(Resolution.X1080, BackgroundColour.None, string.Empty, FileType.Png);

            // Act
            _sut.MutateImage(request);

            // Assert
            _mockImageProcessService.Verify(service => service.ChangeBackgroundColour(It.IsAny<Image>(), It.IsAny<string>()), Times.Never);
        }


        [Fact]
        public void GivenAWatermarkWhenImageResizedThenWatermarkApplied()
        {
            // Arrange
            var request = new ResizeRequest(Resolution.X1080, BackgroundColour.None, "watermark", FileType.Png);

            // Act
            _sut.MutateImage(request);

            // Assert
            _mockImageProcessService.Verify(service => service.AddWaterMark(It.IsAny<Image>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GivenNoWatermarkWhenImageResizedThenNoWatermarkApplied()
        {
            // Arrange
            var request = new ResizeRequest(Resolution.X1080, BackgroundColour.None, string.Empty, FileType.Png);

            // Act
            _sut.MutateImage(request);

            // Assert
            _mockImageProcessService.Verify(service => service.AddWaterMark(It.IsAny<Image>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void GivenFileTypePngWhenImageResizedThenFileTypeSavedAsPng()
        {
            // Arrange
            var request = new ResizeRequest(Resolution.X1080, BackgroundColour.None, String.Empty, FileType.Png);

            // Act
            var result = _sut.MutateImage(request);

            using var image = Image.Load(result, out var format);

            // Assert 
            Assert.Equal("png", format.FileExtensions.FirstOrDefault());
        }

        [Fact]
        public void GivenFileTypeJpgWhenImageResizedThenFileTypeSavedAsJpg()
        {
            // Arrange
            var request = new ResizeRequest(Resolution.X1080, BackgroundColour.None, String.Empty, FileType.Jpg);

            // Act
            var result = _sut.MutateImage(request);

            using var image = Image.Load(result, out var format);

            // Assert 
            Assert.Equal("jpg", format.FileExtensions.FirstOrDefault());
        }
    }
}
