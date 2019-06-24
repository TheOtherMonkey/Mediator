using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Extension methods for the <see cref="IHandleRequests{TRequest,TResponse}"/> interface.
    /// </summary>
    internal static class HandleRequestsExtensions
    {
        /// <summary>
        /// Create the delegate that will be used to executed the <see cref="IHandleRequests{TRequest,TResponse}.Handle"/>
        /// for the identified <paramref name="request"/>.
        /// </summary>
        /// <typeparam name="TRequest">The type of request.</typeparam>
        /// <typeparam name="TResponse">The type response returned from the request.</typeparam>
        /// <param name="handler">The object responsible for handling the <typeparamref name="TRequest"/>.</param>
        /// <param name="request">The request being handled.</param>
        /// <returns>The delegate that will be used to execute <see cref="IHandleRequests{TRequest,TResponse}.Handle"/>
        /// for the <paramref name="request"/>.</returns>
        public static HandleRequestsDelegate<TResponse> HandleRequest<TRequest, TResponse>
            (this IHandleRequests<TRequest, TResponse> handler, TRequest request)
            where TRequest : IAmARequest<TResponse>
        {
            Task<TResponse> HandleRequest() => handler.Handle(request);
            return HandleRequest;
        }
    }
}
