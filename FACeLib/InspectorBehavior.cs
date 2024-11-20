using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

internal class InspectorBehavior : IEndpointBehavior
{
    private readonly string _certificatPath;
    private readonly string _certificatePassword;
    public InspectorBehavior(string certificatPath, string certificatePassword)
    {
        _certificatPath = certificatPath;
        _certificatePassword = certificatePassword;
    }

    public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
    {
        //throw new System.NotImplementedException();
    }

    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    {
        clientRuntime.MessageInspectors.Add(new MessageInspector(_certificatPath, _certificatePassword));
    }

    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
    {
        //throw new System.NotImplementedException();
    }

    public void Validate(ServiceEndpoint endpoint)
    {
        //throw new System.NotImplementedException();
    }
}