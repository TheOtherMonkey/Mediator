using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Abstract base class for a decorator that will perform an action before or after the appropriate
    /// <see cref="IHandleRequests{TRequest, TResponse}"/> is executed.
    /// </summary>
    /// <typeparam name="TRequest">The type that identifies the class that represents a request.</typeparam>
    /// <typeparam name="TResponse">The type that identifies the class that represents the response from the request.</typeparam>
    public abstract class PipelineBehaviour<TRequest, TResponse> :
        IHandleRequests<TRequest, TResponse>
        where TRequest : IAmARequest<TResponse>
    {
        private IHandleRequests<TRequest, TResponse> _inner;

        /// <summary>
        /// Construct a new instance of the <see cref="PipelineBehaviour{TRequest, TResponse}"/> decorator class.
        /// </summary>
        /// <param name="inner">The next <see cref="IHandleRequests{TRequest, TResponse}"/> in the chain.</param>
        protected PipelineBehaviour(IHandleRequests<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        /// <inheritdoc cref="IHandleRequests{TRequest, TResponse}"/>
        public async Task<TResponse> Handle(TRequest request)
        {
            PreHandle(request);
            var response = await HandleRequest(request);
            PostHandle(request, response);

            return response;
        }

        /// <summary>
        /// Invoke the handler for the next item in the chain.
        /// </summary>
        /// <typeparam name="TRequest">The type that identifies the class that represents a request.</typeparam>
        /// <returns>The <typeparamref name="TResponse"/> from the <typeparamref name="TRequest"/>.</returns>
        protected virtual async Task<TResponse> HandleRequest(TRequest request)
        {
            return await _inner.Handle(request);
        }

        /// <summary>
        /// Perform an action prior to the decorated (inner) <see cref="IHandleRequests{TRequest, TResponse}"/> being invoked.
        /// </summary>
        /// <typeparam name="TRequest">The type that identifies the class that represents a request.</typeparam>
        protected virtual void PreHandle(TRequest request)
        { }

        /// <summary>
        /// Perform actions after the decorated (inner) <see cref="IHandleRequests{TRequest, TResponse}"/> has been invoked.
        /// </summary>
        /// <typeparam name="TRequest">The type that identifies the class that represents a request.</typeparam>
        /// <typeparam name="TResponse">The type that identifies the class that represents the response from the request.</typeparam>
        protected virtual void PostHandle(TRequest request, TResponse response)
        { }
    }
}
