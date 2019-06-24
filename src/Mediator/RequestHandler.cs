using System.Linq;
using System.Threading.Tasks;

namespace Mediator
{
    internal abstract class RequestHandler<TResponse>
    {
        public abstract Task<TResponse> Handle(IAmARequest<TResponse> request, IServiceFactory factory);
    }

    /// <summary>
    /// Internal class used to to ensure that the <see cref="IHandleRequests{TRequest, TResponse}"/>
    /// that is to be constructed by <see cref="IServiceFactory.GetInstance{T}"/> is constructed using
    /// the <typeparamref name="TResponse"/> actual (base) type rather than <see cref="IAmARequest{TResponse}"/>.
    /// </summary>
    /// <typeparam name="TRequest">The request.</typeparam>
    /// <typeparam name="TResponse">The response to the request.</typeparam>
    internal class RequestHandlerImpl<TRequest, TResponse> :
        RequestHandler<TResponse> 
        where TRequest : IAmARequest<TResponse>
    {
        /// <summary>
        /// Make the request to the actual <see cref="IHandleRequests{TRequest, TResponse}"/>
        /// implementation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="factory">The response to that request.</param>
        /// <returns>The <typeparamref name="TResponse"/> from the <typeparamref name="TRequest"/>.</returns>
        public override async Task<TResponse> Handle(IAmARequest<TResponse> request, IServiceFactory factory)
        {
            return await Handle((TRequest) request, factory);
        }

        private static async Task<TResponse> Handle(TRequest request, IServiceFactory factory)
        {
            var handler = factory.GetRequestHandler<TRequest, TResponse>();
            var pipelineBehaviors = factory.GetPipeLineBehaviors<TRequest, TResponse>(request).Reverse();
            var pipeLine = pipelineBehaviors.Aggregate(handler.HandleRequest(request), (next, pipe) => () => pipe.Handle(request, next));

            return await pipeLine();
        }
    }
}
