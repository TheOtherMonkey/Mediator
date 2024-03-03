namespace Mediator.Test.Components.RequestHandlerDecorators
{
    // ReSharper disable once UnusedMember.Global
    public class PostRequestDecorator<TRequest, TResponse> : 
        RequestHandlerDecorator<TRequest, TResponse> 
        where TRequest : IAmARequest<TResponse>
    {
        public PostRequestDecorator(IHandleRequests<TRequest, TResponse> inner) : base(inner)
        {}

        protected override void PostInnerHandleInvocation(TRequest request, TResponse response)
        {
            Actual.Pipeline.Enqueue(typeof(PostRequestDecorator<TRequest, TResponse>));
        }
    }
}
