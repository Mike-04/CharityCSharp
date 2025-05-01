using System;
using System.Reflection;
using Charity.Network.gRpc;
using Charity.Repo;
using Charity.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Charity.Server;

internal class Program
{
    private static void Main(string[] args)
    {
        var configPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "config.json");
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(configPath)
            .Build();

        var logFilePath = configuration.GetSection("Logging:LogFilePath").Value;
        var consoleLog = configuration.GetSection("ConfigLog:ConsoleLog").Get<bool>();
        var fileLog = configuration.GetSection("ConfigLog:FileLog").Get<bool>();
        var logLevel = configuration.GetSection("ConfigLog:LogLevel").Value;
        var logFormat = configuration.GetSection("ConfigLog:LogFormat").Value;
        var logFileFormat = configuration.GetSection("ConfigLog:LogFileFormat").Value;
        var port = configuration.GetSection("Server:Port").Value;
        var host = configuration.GetSection("Server:Host").Value;
        
        var loggerConfiguration = new LoggerConfiguration();

        if (fileLog)
            loggerConfiguration.WriteTo.File(logFilePath, outputTemplate: logFileFormat);
        if (consoleLog)
            loggerConfiguration.WriteTo.Console(outputTemplate: logFormat);

        Log.Logger = loggerConfiguration
            .MinimumLevel.Is(Enum.Parse<Serilog.Events.LogEventLevel>(logLevel, true))
            .CreateLogger();

        var loggerFactory = LoggerFactory.Create(builder =>
        {
            if (consoleLog)
                builder.AddConsole();
            if (fileLog)
                builder.AddSerilog();
        });

        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogInformation("Application started");

        var jdbcUtils = new JdbcUtils(configuration, loggerFactory.CreateLogger<JdbcUtils>());

        Console.WriteLine(jdbcUtils.GetConnection());

        UserDBRepository userDBRepository = new UserDBRepository(jdbcUtils, loggerFactory.CreateLogger<UserDBRepository>());
        UserService userService = new UserService(userDBRepository);
        CazCaritabilDBRepository cazCaritabilDBRepository = new CazCaritabilDBRepository(jdbcUtils, loggerFactory.CreateLogger<CazCaritabilDBRepository>());
        CazCaritabilService cazCaritabilService = new CazCaritabilService(cazCaritabilDBRepository);
        DonatorDBRepository donatorDBRepository = new DonatorDBRepository(jdbcUtils, loggerFactory.CreateLogger<DonatorDBRepository>());
        DonatorService donatorService = new DonatorService(donatorDBRepository);
        DonatieDBRepository donatieDBRepository = new DonatieDBRepository(jdbcUtils, loggerFactory.CreateLogger<DonatieDBRepository>());
        DonatieService donatieService = new DonatieService(donatieDBRepository);

        Service.Service service = new Service.Service(userService, cazCaritabilService,donatorService, donatieService);
        
        
        var server = new RpcServer(new RpcService(service), int.Parse(port),host);
        server.run();
        
        Console.WriteLine($"Server started on {host}:{port}");
        Console.WriteLine("Press any key to stop the server...");
        Console.ReadKey();
        Environment.Exit(0);
    }
}

