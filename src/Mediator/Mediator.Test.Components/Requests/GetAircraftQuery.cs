using System.Collections.Generic;
using Mediator.Test.Components.Responses;

namespace Mediator.Test.Components.Requests
{
    public class GetAircraftQuery : IAmARequest<List<Aircraft>>
    {}
}