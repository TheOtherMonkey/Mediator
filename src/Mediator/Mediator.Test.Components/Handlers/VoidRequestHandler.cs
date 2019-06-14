using System.Threading.Tasks;
using Mediator.Test.Components.Requests;

namespace Mediator.Test.Components.Handlers
{
    public class VoidRequestHandler : VoidRequestHandler<VoidRequest>
    {
        public static bool RequestHandled { get; private set; }

        public VoidRequestHandler()
        {
            RequestHandled = false;
        }

        protected override Task HandleRequest(VoidRequest request)
        {
            RequestHandled = true;
            return Task.CompletedTask;
        }
    }
}