using System.Collections.Generic;
using System.Threading.Tasks;
using Mediator.Test.Components.Requests;
using Mediator.Test.Components.Responses;

namespace Mediator.Test.Components.RequestHandlers
{
    public class GetFlightsQueryHandler : IHandleRequests<GetFlightsQuery, List<Flight>>
    {
        public Task<List<Flight>> Handle(GetFlightsQuery request)
        {
            Actual.Pipeline.Enqueue(typeof(GetFlightsQueryHandler));
            return Task.FromResult(new List<Flight>());
        }
    }
}