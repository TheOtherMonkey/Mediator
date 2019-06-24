using System.Collections.Generic;
using System.Threading.Tasks;
using Mediator.Test.Components.Requests;
using Mediator.Test.Components.Responses;

namespace Mediator.Test.Components.Handlers
{
    public class GetAircraftQueryHandler : IHandleRequests<GetAircraftQuery, List<Aircraft>>
    {
        public static  bool RequestHandled { get; private set; }

        public Task<List<Aircraft>> Handle(GetAircraftQuery request)
        {
            RequestHandled = true;
            return Task.FromResult(new List<Aircraft>());
        }
    }
}