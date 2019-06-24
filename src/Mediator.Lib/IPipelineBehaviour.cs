using System.Threading.Tasks;

namespace Mediator
{

    /// <summary>
    /// Delegate for the async continuation of the execution of the pipeline.
    /// </summary>
    /// <typeparam name="TResponse">Response type for the delegate.</typeparam>
    /// <returns>Awaitable task for <typeparamref name="TResponse"/>.</returns>
    public delegate Task<TResponse> HandleRequestsDelegate<TResponse>();

    /// <summary>
    /// Interface for the pipeline behaviors surrounding an inner <see cref="IHandleRequests{TRequest,TResponse}"/>.
    /// </summary>
    /// <typeparam name="TRequest">The type that identifies the class that represents a request.</typeparam>
    /// <typeparam name="TResponse">The type that identifies the class that represents the response from the request.</typeparam>
    public interface IPipelineBehaviour<in TRequest, TResponse> where TRequest : IAmARequest<TResponse>
    {
        /// <summary>
        /// Perform some actions prior to the invoking the the next <see cref="HandleRequestsDelegate{TResponse}"/>
        /// in the pipeline.
        /// </summary>
        /// <param name="request">The request that initiated the pipeline.</param>
        /// <param name="next">The next component in the pipeline.</param>
        /// <returns>the response from the <paramref name="next"/> item in the pipeline.</returns>
        Task<TResponse> Handle(TRequest request, HandleRequestsDelegate<TResponse> next);
    }
}
