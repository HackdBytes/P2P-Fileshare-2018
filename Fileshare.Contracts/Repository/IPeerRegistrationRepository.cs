using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.PeerToPeer;

namespace Fileshare.Contracts.Repository
{
    public interface IPeerRegistrationRepository
    {
        bool IsPeerRegistered { get; }
        string PeerUri { get; }
        PeerName PeerName { get; set; }
        void StartPeerRegistration(string username, int port);
        void StopPeerRegistration();
    }
}
