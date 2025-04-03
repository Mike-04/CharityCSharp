using System.Net.Sockets;
using Charity.Network.Client;
using Charity.Network.Server;
using Charity.Service;

namespace Charity.Server;

internal class Server : ConcurrentServer
{
    private readonly IAppService _Server;

    // WHY ?
    // private ChatClientWorker worker; 

    public Server(string host, int port, IAppService server) : base(host, port)
    {
        _Server = server;
        Console.WriteLine("Created server...");
    }
    protected override Thread CreateWorker(TcpClient client)
    {
        var Worker = new ClientObjectWorker(_Server, client);
        return new Thread(() => Worker.Run()); 
    }
}