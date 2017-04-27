using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WcfHeaderInjection
{
    public class EndpointBehavior : IEndpointBehavior
    {
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

        public EndpointBehavior(Dictionary<string, string> headers)
        {
            if (_headers != null)
            {
                // Make a clone.
                _headers = headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            var inspector = new ClientMessageInspector(_headers);
            clientRuntime.MessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}