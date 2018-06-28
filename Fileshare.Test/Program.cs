using Fileshare.Contracts.Repository;
using Fileshare.Contracts.Services;
using Fileshare.Domain.Models;
using Fileshare.Logics.PnrpManager;
using Fileshare.Logics.ServiceManager;
using System;
using System.Net;
using System.Diagnostics;
using System.Linq;

namespace Fileshare.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Count() <= 1)
            {
                Process.Start("Fileshare.Test.exe");
            }

            new Program().Run();
        }

        private void Run()
        {
            Peer<IPingService> peer = new Peer<IPingService> { UserName = Guid.NewGuid().ToString().Split('-')[4] };
            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolver = new PeerNameResolver(peer.UserName);
            IPeerConfigurationService peerConfigurationService = new PeerConfigurationService(peer);
            peerRegistration.StartPeerRegistration(peer.UserName, peerConfigurationService.Port);

            Console.WriteLine("Peer Information . . . .");
            Console.WriteLine($"Peer Uri: {peerRegistration.PeerUri} \t\t Port: {peerConfigurationService.Port}");
            var host = Dns.GetHostEntry(peerRegistration.PeerUri);
            host.AddressList?.ToList().ForEach(p => Console.WriteLine($"\t\t IP :{p}"));
            Console.ReadLine();
        }
    }
}
