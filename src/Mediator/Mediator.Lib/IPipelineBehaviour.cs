using System.Threading.Tasks;

namespace Mediator
{
    /// <summary>
    /// Delegate for the async continuation of the execution of the pipeline.
    /// </summary>
    /// <typeparam name="TResponse">Response type for the delegate.</typeparam>
    /// <returns>Awaitable task for <typeparamref name="TResponse"/>.</returns>
    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();

    /// <summary>
    /// Interface for the pipeline behaviors surrounding an inner <see cref="IHandleRequests{TRequest,TResponse}"/>.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IPipelineBehaviour<in TRequest, TResponse> where TRequest : IAmARequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next);
    }
}
