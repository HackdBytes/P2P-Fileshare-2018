using System.Net;
using System.Net.PeerToPeer;

namespace Fileshare.Domain.Models
{
    public class PeerEndPointsCollection
    {
        public PeerEndPointsCollection(PeerName peer, IPEndPointCollection ipEndPoint)
        {
            PeerHostName = peer;
            PeerEndPoints = ipEndPoint;
        }

        public PeerName PeerHostName { get; }
        public IPEndPointCollection PeerEndPoints { get; }
    }
}
