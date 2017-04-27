using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;

namespace WcfHeaderInjection
{
    public class EndpointBehaviorExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof (EndpointBehavior);

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public KeyValueConfigurationCollection Headers => (KeyValueConfigurationCollection) base[""];

        protected override object CreateBehavior()
        {
            var headers = Headers;
            var injectableHeaders = Headers?.AllKeys.ToDictionary(key => key, key => headers[key].Value);
            return new EndpointBehavior(injectableHeaders);
        }
    }
}