using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ImageResize.Enums;
using ImageResize.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;

namespace ImageResize.Controllers
{
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private IMediator _mediator;

        public ImageController(ILogger<ImageController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{controller}/resize")]
        public async Task<IActionResult> Get(string resolution, string backgroundColour, string watermark, string imageFileType)
        {
            if (string.IsNullOrEmpty(resolution) || !Enum.TryParse(resolution, out Resolution resolutionEnum))
            {
                return BadRequest($"invalid resolution provided - ${resolution}");
            }

            if (!Enum.TryParse(backgroundColour, out BackgroundColour backgroundColourEnum))
            {
                return BadRequest($"invalid background colour provided - ${backgroundColour}");
            }

            if (string.IsNullOrEmpty(backgroundColour))
            {
                backgroundColourEnum = BackgroundColour.None;
            }

            if (string.IsNullOrEmpty(imageFileType) || !Enum.TryParse(imageFileType, out FileType imageFileTypeEnum))
            {
                return BadRequest($"invalid image file type provided - ${imageFileType}");
            }

            var resizedImage =
                    await _mediator.Send(new ResizeRequest(resolutionEnum, backgroundColourEnum, watermark,
                        imageFileTypeEnum));


            return File(resizedImage, $"image/{imageFileTypeEnum.GetDisplayName()}");
        }
    }
}
