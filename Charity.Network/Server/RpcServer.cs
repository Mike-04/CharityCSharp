using System.Net;
using Charity.Network.gRpc;
using Charity.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class RpcServer(RpcService service,  int port, string host)
{
    private readonly RpcService _service = service;
    private readonly int _port = port;
    private readonly string _host = host;
    
    public void run()
    {
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .ConfigureServices(services =>
                    {
                        services.AddGrpc();
                        services.AddSingleton<RpcService>(_service);
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGrpcService<RpcService>();

                        });
                    }).ConfigureKestrel(options =>
                    {
                        options.Listen(IPAddress.Parse(_host), _port, listenOptions =>
                        {
                            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
                        });
                    });
            }).Build().Run();
    }
}