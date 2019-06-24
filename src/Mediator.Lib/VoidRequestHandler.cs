using System.Threading.Tasks;
// ReSharper disable UnusedParameter.Global

namespace Mediator
{
    /// <summary>
    /// Abstract handler for a <see cref="IHandleRequests{TRequest,TResponse}"/> that returns
    /// no response.
    /// </summary>
    /// <typeparam name="TRequest">The type that identifies the class that represents a request.</typeparam>
    public abstract class VoidRequestHandler<TRequest> : 
        IHandleRequests<TRequest> 
        where TRequest : IAmARequest<Void>
    {
        /// <inheritdoc cref="IHandleRequests{TRequest}"/>
        /// <returns>a new <see cref="Void"/> response.</returns>
        public async Task<Void> Handle(TRequest request)
        {
            await HandleRequest(request);
            return Void.Instance;
        }

        /// <summary>
        /// Invoke the behaviour required for the request.
        /// </summary>
        /// <param name="request">The request being made.</param>
        protected abstract Task HandleRequest(TRequest request);
    }
}
