using System.Collections.Generic;
using Mediator.Test.Components.Responses;

namespace Mediator.Test.Components.Requests
{
    public class GetFlightsQuery : IAmARequest<List<Flight>>
    {
        public Aircraft Aircraft { get; set; }
    }
}