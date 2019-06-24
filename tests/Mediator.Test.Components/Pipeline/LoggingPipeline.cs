using System.Threading.Tasks;

namespace Mediator.Test.Components.Pipeline
{
    public abstract class LoggingHandler
    {
        public static bool RequestHandled { get; internal set; }
    }

    public class LoggingPipeline<TRequest, TResponse> :
        PipelineBehaviour<TRequest, TResponse>
        where TRequest : IAmARequest<TResponse>
    {
        public LoggingPipeline()
        {
            LoggingHandler.RequestHandled = false;
        }

        protected override void PostHandle(TRequest request, TResponse response)
        {
            LoggingHandler.RequestHandled = true;
        }
    }

    public class LoggingDecorator<TRequest, TResponse>
        : IHandleRequests<TRequest, TResponse>
        where TRequest : IAmARequest<TResponse>
    {
        private readonly IHandleRequests<TRequest, TResponse> _inner;

        public LoggingDecorator(IHandleRequests<TRequest, TResponse> inner)
        {
            _inner = inner;
            LoggingHandler.RequestHandled = false;
        }

        public async Task<TResponse> Handle(TRequest request)
        {
            var response = await _inner.Handle(request);
            LoggingHandler.RequestHandled = true;

            return response;
        }
    }
}