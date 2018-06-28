using Fileshare.Contracts.Services;
using Fileshare.Domain.Models;
using System;
using System.Net;
using System.Net.PeerToPeer;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Fileshare.Logics.ServiceManager
{
    public class PeerConfigurationService : IPeerConfigurationService
    {
        #region fields
        private int _port;
        private ICommunicationObject Communication;
        private DuplexChannelFactory<IPingService> _factory;
        private bool _isServiceStarted;
        #endregion

        #region Cto
        public PeerConfigurationService(Peer<IPingService> peer)
        {
            Peer = peer;
        }
        #endregion

        public int Port => FindFreePort();

        private int FindFreePort()
        {
            int port;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
            {
                socket.Bind(endPoint);
                IPEndPoint local = (IPEndPoint)socket.LocalEndPoint;
                port = local.Port;
            }

            if (port == 0)
                throw new ArgumentNullException(nameof(port));

            return port;
        }

        public Peer<IPingService> Peer { get; }

        public bool StopPeerService()
        {
            if (Communication != null)
            {
                Communication.Close();
                Communication = null;
                _factory = null;
                return true;
            }

            return false;
        }

        public bool StartPeerService()
        {
#pragma warning disable 618
            var binding = new NetPeerTcpBinding
            {
                Security = { Mode = SecurityMode.None }
            };
#pragma warning restore 618

            var endPoint = new ServiceEndpoint(
                ContractDescription.GetContract(typeof(IPingService)),
                binding,
                new EndpointAddress("net.p2p://FileshareP2P")); /* feel free to change this or create a commandline argument */
 
            Peer.Host = new PingService();
            _factory = new DuplexChannelFactory<IPingService>(new InstanceContext(Peer.Host), endPoint);
            Peer.Channel = _factory.CreateChannel();
            Communication = (ICommunicationObject)Peer.Channel;

            if (Communication != null)
            {
                Communication.Opened += CommunicationOnOpened;
                try
                {
                    Communication.Open();
                    if (_isServiceStarted)
                        return _isServiceStarted;
                }
                catch (PeerToPeerException e)
                {
                    throw new PeerToPeerException("error establishing peer services {0}", e);
                }
            }

            return _isServiceStarted;
        }

        private void CommunicationOnOpened(object sender, EventArgs eventArgs)
        {
            _isServiceStarted = true;
        }
    }
}
