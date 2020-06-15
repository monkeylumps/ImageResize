using ImageResize.Requests;

namespace ImageResize.Services
{
    public interface IImageService
    {
        byte[] MutateImage(ResizeRequest request);
    }
}
