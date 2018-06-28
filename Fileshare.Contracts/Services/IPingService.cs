using System;
using System.ServiceModel;


namespace Fileshare.Contracts.Services
{
    [ServiceContract(CallbackContract = typeof(IPingService))]
    public interface IPingService
    {
        [OperationContract(IsOneWay = true)]
        void Ping(int port, string peerUri);
    }
}
