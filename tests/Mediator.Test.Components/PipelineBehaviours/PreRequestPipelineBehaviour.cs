namespace Mediator.Test.Components.PipelineBehaviours
{
    public class PreRequestPipelineBehaviour<TRequest, TResponse> :
        PipelineBehaviour<TRequest, TResponse>
        where TRequest : IAmARequest<TResponse>
    {
        protected override void PreHandle(TRequest request)
        {
            Actual.Pipeline.Enqueue(typeof(PreRequestPipelineBehaviour<TRequest, TResponse>));
        }
    }
}