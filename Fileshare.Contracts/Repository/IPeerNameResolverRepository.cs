using Fileshare.Domain.Models;

namespace Fileshare.Contracts.Repository
{
    public interface IPeerNameResolverRepository
    {
        void ResolvePeerName();
        PeerEndPointsCollection PeerEndPointCollection { get; set; }
    }
}
