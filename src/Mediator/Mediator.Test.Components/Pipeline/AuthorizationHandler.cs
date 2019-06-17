using System.Threading.Tasks;

namespace Mediator.Test.Components.Pipeline
{
    public abstract class AuthorizationHandler
    {
        public static bool RequestHandled;
    }

    public class AuthorizationHandler<TRequest, TResponse> :
        PipelineBehaviour<TRequest, TResponse>
        where TRequest : IAmARequest<TResponse>
    {
        
        public AuthorizationHandler(IHandleRequests<TRequest, TResponse> inner) : base(inner)
        {
            AuthorizationHandler.RequestHandled = false;
        }

        protected override void PostHandle(TRequest request, TResponse response)
        {
            AuthorizationHandler.RequestHandled = true;
        }        
    }
}