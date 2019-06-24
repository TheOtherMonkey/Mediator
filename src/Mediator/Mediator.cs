using System;
using System.Threading.Tasks;
using Aggregator;

namespace Mediator
{
    /// <summary>
    /// Very simplistic implementation of the Mediator Design Pattern. Allows
    /// asynchronous CQRS requests to be made that are fulfilled by objects that implement
    /// the <see cref="IHandleRequests{TRequest, TResponse}"/> interface.
    /// Also incorporates an <see cref="IAggregateMessages"/> to hand the publish/subscribe metaphor.
    /// </summary>
    public class Mediator : IMediate
    {
        private readonly IServiceFactory _factory;
        private readonly IAggregateMessages _aggregateMessages;

        /// <summary>
        /// Construct a new instance of the <see cref="Mediator"/> class.
        /// </summary>
        /// <param name="factory">The object that is used wrap the IoC container of choice and used
        /// to construct the required implementors of the <see cref="IHandleRequests{TRequest, TResponse}"/>
        /// interfaces.</param>
        /// <param name="aggregateMessages">The in-memory event aggregateMessages.</param>
        public Mediator(IServiceFactory factory, IAggregateMessages aggregateMessages)
        {
            _factory = factory;
            _aggregateMessages = aggregateMessages;
        }

        /// <inheritdoc cref="IMediate" />
        public async Task<TResponse> Request<TResponse>(IAmARequest<TResponse> request)
        {
            var handlerType = typeof(RequestHandlerImpl<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = Activator.CreateInstance(handlerType);

            return await((RequestHandler<TResponse>)handler)
                .Handle(request, _factory)
                .ConfigureAwait(false);
        }

        /// <inheritdoc cref="IMediate" />
        public async Task Send<TRequest>(TRequest request) where TRequest : IAmARequest
        {
            await Request(request);
        }

        /// <inheritdoc cref="IMediate" />
        public async Task Publish<T>(T message) where T : IAmAMessage
        {
            await _aggregateMessages.Publish(message).ConfigureAwait(false);
        }
    }
}
