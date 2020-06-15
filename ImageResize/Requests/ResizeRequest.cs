using ImageResize.Enums;
using MediatR;

namespace ImageResize.Requests
{
    public class ResizeRequest : IRequest, IRequest<string>
    {
        public ResizeRequest(Resolution resolution, BackgroundColour backgroundColour, string watermark, FileType fileType)
        {
            Resolution = resolution;
            BackgroundColour = backgroundColour;
            Watermark = watermark;
            FileType = fileType;
        }

        public Resolution Resolution { get; private set; }
        public BackgroundColour BackgroundColour { get; private set; }
        public string Watermark { get; private set; }
        public FileType FileType { get; private  set; }
    }
}
