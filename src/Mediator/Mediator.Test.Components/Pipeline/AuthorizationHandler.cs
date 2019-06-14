using System.Threading.Tasks;

namespace Mediator.Test.Components.Pipeline
{
    public abstract class AuthorizationHandler
    {
        public static bool RequestHandled;
    }

    public class AuthorizationHandler<TRequest, TResponse> :
        AuthorizationHandler,
        IHandleRequests<TRequest, TResponse>
        where TRequest : IAmARequest<TResponse>
    {
        private readonly IHandleRequests<TRequest, TResponse> _inner;

        public AuthorizationHandler(IHandleRequests<TRequest, TResponse> inner)
        {
            _inner = inner;
            RequestHandled = false;
        }

        public async Task<TResponse> Handle(TRequest request)
        {
            RequestHandled = true;
            return await _inner.Handle(request);
        }
    }
}