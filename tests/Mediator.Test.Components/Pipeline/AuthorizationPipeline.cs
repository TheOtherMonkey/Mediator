using System.Threading.Tasks;

namespace Mediator.Test.Components.Pipeline
{
    public abstract class AuthorizationHandler
    {
        public static bool RequestHandled { get; internal set; }
    }

    public class AuthorizationPipeline<TRequest, TResponse> :
        PipelineBehaviour<TRequest, TResponse>
        where TRequest : IAmARequest<TResponse>
    {
        public AuthorizationPipeline()
        {
            AuthorizationHandler.RequestHandled = false;
        }

        protected override void PreHandle(TRequest request)
        {
            AuthorizationHandler.RequestHandled = true;
        }
    }

    public class AuthorizationDecorator<TRequest, TResponse> 
        : RequestHandlerDecorator<TRequest, TResponse>
        where TRequest : IAmARequest<TResponse>
    {
        public AuthorizationDecorator(IHandleRequests<TRequest, TResponse> inner) : base(inner)
        {
            AuthorizationHandler.RequestHandled = false;
        }

        public override async Task<TResponse> Handle(TRequest request)
        {
            AuthorizationHandler.RequestHandled = true;
            return await Inner.Handle(request);}
    }
}