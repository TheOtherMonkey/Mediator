namespace Mediator.Test.Components.RequestHandlerDecorators
{
    // ReSharper disable once UnusedMember.Global
    public class PreRequestDecorator<TRequest, TResponse> :
        RequestHandlerDecorator<TRequest, TResponse>
        where TRequest : IAmARequest<TResponse>
    {
        public PreRequestDecorator(IHandleRequests<TRequest, TResponse> inner) : base(inner)
        {}

        protected override void PreInnerHandleInvocation(TRequest request)
        {
            Actual.Pipeline.Enqueue(typeof(PreRequestDecorator<TRequest, TResponse>));
        }
    }
}
