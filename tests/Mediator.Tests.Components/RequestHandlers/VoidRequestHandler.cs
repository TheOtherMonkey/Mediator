using Mediator.Test.Components.Requests;

namespace Mediator.Test.Components.RequestHandlers
{
    public class VoidRequestHandler : VoidRequestHandler<VoidRequest>
    {
        protected override Task HandleRequest(VoidRequest request)
        {
            Actual.Pipeline.Enqueue(typeof(VoidRequestHandler));
            return Task.CompletedTask;
        }
    }
}