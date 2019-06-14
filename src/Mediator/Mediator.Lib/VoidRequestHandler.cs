using System.Threading.Tasks;

namespace Mediator
{
    public abstract class VoidRequestHandler<TRequest> : 
        IHandleRequests<TRequest> 
        where TRequest : IAmARequest<Void>
    {
        public async Task<Void> Handle(TRequest request)
        {
            await HandleRequest(request);
            return Void.Instance;
        }

        protected abstract Task HandleRequest(TRequest request);
    }
}
