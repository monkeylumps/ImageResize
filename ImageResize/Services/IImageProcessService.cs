using SixLabors.ImageSharp;

namespace ImageResize.Services
{
    public interface IImageProcessService
    {
        void AddWaterMark(Image image, string watermark);
        void ChangeBackgroundColour(Image image, string colourHex);
        void ResizeImage(Image image, string resolution);
    }
}
