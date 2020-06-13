using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ImageResize.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageResize.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string resolution, string backgroundColour, string watermark, string imageFileType)
        {
            if(string.IsNullOrEmpty(resolution) || !Enum.TryParse(resolution, out Resolution resolutionEnum))
            {
                return BadRequest($"invalid resolution provided - ${resolution}");
            }

            if(!string.IsNullOrEmpty(backgroundColour) && !Enum.TryParse(backgroundColour, out BackgroundColour backgroundColourEnum))
            {
                return BadRequest($"invalid background colour provided - ${backgroundColour}");
            }

            if (string.IsNullOrEmpty(imageFileType) || !Enum.TryParse(imageFileType, out FileType imageFileTypeEnum))
            {
                return BadRequest($"invalid image file type provided - ${imageFileType}");
            }



            return NoContent();
        }
    }
}
