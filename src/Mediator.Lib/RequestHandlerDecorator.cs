using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Decorator for a <see cref="IHandleRequests{TRequest,TResponse}"/> that will be
    /// wrapped around to form a pipeline of behavior.
    /// </summary>
    /// <typeparam name="TRequest">The type that identifies the class that represents a request.</typeparam>
    /// <typeparam name="TResponse">The type that identifies the class that represents the response from the request.</typeparam>
    public abstract class RequestHandlerDecorator<TRequest, TResponse> : 
        IHandleRequests<TRequest, TResponse> 
        where TRequest : IAmARequest<TResponse>
    {
        /// <summary>
        /// The request that is being decorated.
        /// </summary>
        protected readonly IHandleRequests<TRequest, TResponse> Inner;

        /// <summary>
        /// Construct a new instance of the <see cref="RequestHandlerDecorator{TRequest,TResponse}"/> class.
        /// </summary>
        /// <param name="inner">the inner/decorated <see cref="IHandleRequests{TRequest,TResponse}"/> object.</param>
        protected RequestHandlerDecorator(IHandleRequests<TRequest, TResponse> inner)
        {
            Inner = inner;
        }

        /// <summary>
        /// Handle the request and provide a response to that request. Allows the decorator to
        /// insert behaviour before and/or after the the decorated <see cref="IHandleRequests{TRequest,TResponse}"/>.
        /// </summary>
        /// <param name="request">The request/query for which a response is required.</param>
        /// <returns>The request to the response.</returns>
        public abstract Task<TResponse> Handle(TRequest request);
    }
}
