using Core.Services;
using Discord.Interactions;
using Discord.WebSocket;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("config.json", false);       // Add the config file to IConfiguration variables
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<DiscordSocketClient>();               // Add the discord client to services
        services.AddSingleton<InteractionService>();                // Add the interaction service to services
        services.AddHostedService<InteractionHandlingService>();    // Add the slash command handler
        services.AddHostedService<DiscordStartupService>();         // Add the discord startup service
    })
    .Build();

await host.RunAsync();