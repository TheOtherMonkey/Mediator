using System.Threading.Tasks;
using Aggregator;
// ReSharper disable UnusedMemberInSuper.Global

namespace Mediator
{
    /// <summary>
    /// Interface for the class that mediates request/responses.
    /// </summary>
    public interface IMediate
    {
        /// <summary>
        /// Make an asynchronous request that will be handled by an object that implements the
        /// <see cref="IHandleRequests{TRequest, TResponse}"/> interface and receive a response.
        /// </summary>
        /// <typeparam name="TResponse">The type that indicates the expected response from the request.</typeparam>
        /// <param name="request">The request being made.</param>
        /// <returns>Object from the <see cref="IHandleRequests{TRequest,TResponse}.Handle"/> method.</returns>
        /// <exception cref="UnableToHandleRequestException">Raised when a request handler has not been registerd to handle 
        /// the <paramref name="request"/>.</exception>
        Task<TResponse> Request<TResponse>(IAmARequest<TResponse> request);

        /// <summary>
        /// Send an asynchronous request that will be handled by an object that implements the
        /// <see cref="IHandleRequests{TRequest, TResponse}"/> interface where <typeparamref name="TRequest"/>
        /// is a <see cref="IAmARequest{Void}"/>.
        /// </summary>
        /// <param name="request">The request being made.</param>
        /// <exception cref="UnableToHandleRequestException">Raised when a request handler has not been registerd to handle 
        /// the <paramref name="request"/>.</exception>
        Task Send<TRequest>(TRequest request) where TRequest : IAmARequest;

        /// <summary>
        /// Asynchronously sends a message through the system to any registered object that implements the relevant
        /// .<see cref="IListenFor{T}"/> interface.
        /// </summary>
        /// <typeparam name="T">The type of message being raised.</typeparam>
        /// <param name="message">The message being sent.</param>        
        Task Publish<T>(T message) where T : IAmAMessage;
    }
}