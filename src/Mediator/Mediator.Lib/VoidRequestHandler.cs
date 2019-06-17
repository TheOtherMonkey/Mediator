using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Abstract class for a request handler that returns a <see cref="Void"/> (null) response.
    /// </summary>
    /// <typeparam name="TRequest">The type of request that will be handled.</typeparam>
    public abstract class VoidRequestHandler<TRequest> : 
        IHandleRequests<TRequest> 
        where TRequest : IAmARequest<Void>
    {
        /// <summary>
        /// Handle the request that is being made.
        /// </summary>
        /// <param name="request">The type of request to be handled.</param>
        /// <returns>an instance of the <see cref="Void"/> class.</returns>
        public async Task<Void> Handle(TRequest request)
        {
            await HandleRequest(request);
            return Void.Instance;
        }

        /// <summary>
        /// Execute/Invoke the handler for the <typeparamref name="TRequest"/>.
        /// </summary>
        /// <param name="request">The type of request to be handled.</param>
        protected abstract Task HandleRequest(TRequest request);
    }
}
