using System.IO;
using System.Net;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Xunit;

namespace ImageResizeIIntegrationTests.Image
{
    public class ImageTests
    {
        private const string BaseUrl = "https://localhost:44314";
        private readonly string _url;

        public ImageTests()
        {
            // Arrange
            _url = $"{BaseUrl}/image/resize";
        }

        [Fact(Skip = "Ignored")]
        public async Task GivenNoParametersMissingWhenImageRequestedThenReturnNoContent()
        {
            // Arrange
            var folderPath = "C:\\downloads";
            var filename = "test.png";

            if(File.Exists($"{folderPath}\\{filename}"))
            {
                File.Delete($"{folderPath}\\{filename}");
            }

            // Act
            await _url
                .SetQueryParams(new
                {
                    resolution = "X1080",
                    backgroundColour = "Black",
                    watermark = "test",
                    imageFileType = "Png"
                })
                .DownloadFileAsync(folderPath, filename);

            // Assert
            Assert.True(File.Exists($"{folderPath}\\{filename}"));
        }

        [Fact(Skip = "Ignored")]
        public async Task GivenResolutionMissingWhenImageRequestedThenReturn400()
        {
            // Act
            var result = await _url
                .AllowAnyHttpStatus()
                .SetQueryParams(new
                {
                    backgroundColour = "",
                    watermark = "",
                    imageFileType = ""
                })
                .GetAsync();

            // Assert 
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact(Skip = "Ignored")]
        public async Task GivenInvalidResolutionWhenImageRequestedThenReturn400()
        {
            // Act
            var result = await _url
                .AllowAnyHttpStatus()
                .SetQueryParams(new
                {
                    resolution = "X720",
                    backgroundColour = "",
                    watermark = "",
                    imageFileType = ""
                })
                .GetAsync();

            // Assert 
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact(Skip = "Ignored")]
        public async Task GivenInvalidBackGroundColourWhenImageRequestedThenReturn400()
        {
            // Act
            var result = await _url
                .AllowAnyHttpStatus()
                .SetQueryParams(new
                {
                    resolution = "X1080",
                    backgroundColour = "Brown",
                    watermark = "",
                    imageFileType = ""
                })
                .GetAsync();

            // Assert 
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact(Skip = "Ignored")]
        public async Task GivenImageFileTypeMissingWhenImageRequestedThenReturn400()
        {
            // Act
            var result = await _url
                .AllowAnyHttpStatus()
                .SetQueryParams(new
                {
                    resolution = "X1080",
                    backgroundColour = "Black",
                    watermark = "Dan"
                })
                .GetAsync();

            // Assert 
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact(Skip = "Ignored")]
        public async Task GivenInvalidImageFileTypeWhenImageRequestedThenReturn400()
        {
            // Act
            var result = await _url
                .AllowAnyHttpStatus()
                .SetQueryParams(new
                {
                    resolution = "X1080",
                    backgroundColour = "Black",
                    watermark = "Dan",
                    imageFileType = "Pnr"
                })
                .GetAsync();

            // Assert 
            Assert.False(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
