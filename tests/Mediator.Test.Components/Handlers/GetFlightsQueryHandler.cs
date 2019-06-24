using System.Collections.Generic;
using System.Threading.Tasks;
using Mediator.Test.Components.Requests;
using Mediator.Test.Components.Responses;

namespace Mediator.Test.Components.Handlers
{
    public class GetFlightsQueryHandler : IHandleRequests<GetFlightsQuery, List<Flight>>
    {
        public static bool RequestHandled { get; private set; }


        public Task<List<Flight>> Handle(GetFlightsQuery request)
        {
            RequestHandled = true;
            return Task.FromResult(new List<Flight>());
        }
    }
}