using Fileshare.Contracts.Services;
using System;

namespace Fileshare.Logics.ServiceManager
{
    public class PingService : IPingService
    {
        public void Ping(int port, string peerUri)
        {
            Console.WriteLine($"Yah, ! From: {peerUri}");
        }
    }
}
