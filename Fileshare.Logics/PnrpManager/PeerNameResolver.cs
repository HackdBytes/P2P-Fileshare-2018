
using System;
using System.Linq;
using System.Net.PeerToPeer;
using Fileshare.Contracts.Repository;
using Fileshare.Domain.Models;

namespace Fileshare.Logics.PnrpManager
{
    public class PeerNameResolver : IPeerNameResolverRepository
    {
        private PeerEndPointsCollection _peers; /* I may have to change back to PeerEndPointCollection */
        private string _username;

        public PeerNameResolver(string username)
        {
            _username = username;
        }

        public void ResolvePeerName()
        {
            if (string.IsNullOrEmpty(_username))
                throw new ArgumentNullException(nameof(_username));

            System.Net.PeerToPeer.PeerNameResolver resolver = new System.Net.PeerToPeer.PeerNameResolver();
            var result = resolver.Resolve(new PeerName(_username), Cloud.AllLinkLocal);
            if (result.Any())
                PeerEndPointCollection = new PeerEndPointsCollection(result[0].PeerName, result[0].EndPointCollection);
        }

        public PeerEndPointsCollection PeerEndPointCollection { get; set; }
    }
}
