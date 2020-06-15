using System.Net;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Xunit;

namespace ImageResizeIIntegrationTests.Image
{
    public class ImageTests
    {
        private string baseUrl = "https://localhost:44314";
        private string url;

        public ImageTests()
        {
            // Arrange
            url = $"{baseUrl}/image/resize";
        }

        [Fact]
        public async Task GivenNoParametersMissingWhenImageRequestedThenReturnNoContent()
        {

            // Act
            var result = await url
                .SetQueryParams(new
                {
                    resolution = "X1080",
                    backgroundColour = "Black",
                    watermark = "Dan",
                    imageFileType = "Png"
                })
                .GetAsync();

            // Assert 
            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact(Skip = "Ignored")]
        //[Fact]
        public async Task GivenResolutionMissingWhenImageRequestedThenReturn400()
        {
            // Act
            var result = await url
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
        //[Fact]
        public async Task GivenInvalidResolutionWhenImageRequestedThenReturn400()
        {
            // Act
            var result = await url
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
        //[Fact]
        public async Task GivenInvalidBackGroundColourWhenImageRequestedThenReturn400()
        {
            // Act
            var result = await url
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
        //[Fact]
        public async Task GivenImageFileTypeMissingWhenImageRequestedThenReturn400()
        {
            // Act
            var result = await url
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
        //[Fact]
        public async Task GivenInvalidImageFileTypeWhenImageRequestedThenReturn400()
        {
            // Act
            var result = await url
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
