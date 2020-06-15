using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ImageResize.Requests
{
    public class ResizeRequestHandler : IRequestHandler<ResizeRequest, string>
    {
        public Task<string> Handle(ResizeRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult("yes");
        }
    }
}
