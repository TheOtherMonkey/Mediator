using Mediator.Test.Components.Requests;
using Mediator.Test.Components.Responses;

namespace Mediator.Test.Components.RequestHandlers
{
    public class GetAircraftQueryHandler : IHandleRequests<GetAircraftQuery, List<Aircraft>>
    {
        public Task<List<Aircraft>> Handle(GetAircraftQuery request)
        {
            Actual.Pipeline.Enqueue(typeof(GetAircraftQueryHandler));
            return Task.FromResult(new List<Aircraft>());
        }
    }
}