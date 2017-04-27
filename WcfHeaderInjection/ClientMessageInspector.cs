using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WcfHeaderInjection
{
    internal class ClientMessageInspector : IClientMessageInspector
    {
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

        public ClientMessageInspector(Dictionary<string, string> headers)
        {
            if (_headers != null)
            {
                // Make a clone.
                _headers = headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request,
            IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequestMessage = null;
            object httpRequestMessageObject;
            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
            {
                httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
            }

            if (httpRequestMessage == null)
            {
                httpRequestMessage = new HttpRequestMessageProperty();
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
            }

            foreach (var header in _headers)
            {
                httpRequestMessage.Headers[header.Key] = header.Value;
            }

            return null;
        }
    }
}