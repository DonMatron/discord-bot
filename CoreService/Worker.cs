using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace CoreService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DiscordSocketConfig _config;
        private readonly DiscordSocketClient _client;
        private readonly LoggingService _loggingService;
        private readonly CommandHandler _commandHandler;

        public Worker(ILogger<Worker> logger, CommandService commandService)
        {
            _logger = logger;
            _config = new DiscordSocketConfig { MessageCacheSize = 100 };
            _client = new DiscordSocketClient(_config);
            _loggingService = new LoggingService(_client, commandService, _logger);
            _commandHandler = new CommandHandler(_client, commandService);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //TODO: move token to safer place
            var token = "MTE5ODc1NzI2ODQ4MTYzODU1MQ.GCNjdu.vZlEfS0GKw5-yESdMLV-RxbEf04I3FGVnwvKiE";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.MessageUpdated += MessageUpdated;
            _client.Ready += () =>
            {
                _logger.LogInformation("Bot is connected!");
                return Task.CompletedTask;
            };

            BlockProcess(stoppingToken);
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            // If the message was not in the cache, downloading it will result in getting a copy of `after`.
            var message = await before.GetOrDownloadAsync();
            _logger.LogInformation($"{message} -> {after}");
        }

        private async void BlockProcess(CancellationToken stoppingToken)
        {
            await Task.Delay(-1, stoppingToken);
        }
    }
}