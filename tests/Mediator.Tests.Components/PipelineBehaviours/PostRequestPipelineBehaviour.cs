namespace Mediator.Test.Components.PipelineBehaviours
{
    public class PostRequestPipelineBehaviour<TRequest, TResponse> :
        PipelineBehaviour<TRequest, TResponse>
        where TRequest : IAmARequest<TResponse>
    {
        protected override void PostHandle(TRequest request, TResponse response)
        {
            Actual.Pipeline.Enqueue(typeof(PostRequestPipelineBehaviour<TRequest, TResponse>));
        }
    }
}