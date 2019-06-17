using System.Threading.Tasks;

namespace Mediator.Test.Components.Pipeline
{
    public class LoggingHandler
    {
        public static bool RequestHandled;
    }

    public class LoggingHandler<TRequest, TResponse> :
        PipelineBehaviour<TRequest, TResponse>
        where TRequest : IAmARequest<TResponse>
    {
        public LoggingHandler(IHandleRequests<TRequest, TResponse> inner) : base(inner)
        {
            LoggingHandler.RequestHandled = false;
        }

        protected override void PostHandle(TRequest request, TResponse response)
        {
            LoggingHandler.RequestHandled = true;
        }
    }
}