using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ImageResize.Requests
{
    public class ResizeRequestHandler : IRequestHandler<ResizeRequest, FileStream>
    {
        public Task<FileStream> Handle(ResizeRequest request, CancellationToken cancellationToken)
        {
            var image = File.OpenRead(
                "G:\\code\\ImageReSize\\ImageResize\\ImageResize\\Images\\01_04_2019_001103.png");

            return Task.FromResult(image);
        }
    }
}
