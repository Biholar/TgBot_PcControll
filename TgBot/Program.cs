using TgBot;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        var options = configuration.GetSection("TgBot").Get<BotOptions>();
        services.AddTelegramClient(options);
        services.AddHostedService<Worker>();
        
        
    })
    .UseWindowsService()
    .Build();

await host.RunAsync();