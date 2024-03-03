using Mediator.Test.Components.Requests;
using Mediator.Test.Components.Responses;

namespace Mediator.Test.Components.RequestHandlers
{
    public class TestRequestHandler : IHandleRequests<TestRequest, TestResponse>
    {
        public Task<TestResponse> Handle(TestRequest request)
        {
            Actual.Pipeline.Enqueue(typeof(TestRequestHandler));
            return Task.FromResult(new TestResponse());
        }
    }
}
