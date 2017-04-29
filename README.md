# WCF Header Injection Endpoint Behavior
Allows injection of custom headers into a WCF client endpoint.

Add the following to <system.serviceModel>. Any headers you wish to add should be placed under the headerInjection element. The dll itself should generally be placed in the same folder as your WCF client.

```xml
<extensions>
  <behaviorExtensions>
    <add name="headerInjection" type="WcfHeaderInjection.EndpointBehaviorExtension, WcfHeaderInjection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
  </behaviorExtensions>
</extensions>

<behaviors>
  <endpointBehaviors>
    <behavior name="MyServiceBehaviour">
      <headerInjection>
        <add key="HeaderKey" value="HeaderValue" />
        <add key="UserAgent" value="MyApp (Windows 2012)" />
      </headerInjection>
    </behavior>
  </endpointBehaviors>
</behaviors>

<client>
  <endpoint address="..." behaviorConfiguration="MyServiceBehaviour" name="MyService" />
</client>
```
