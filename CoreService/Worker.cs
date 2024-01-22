using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace CoreService
{
    public class Worker : BackgroundService
    {

        private readonly DiscordSocketClient _client;

        private readonly CommandService _commandService;

        private readonly LoggingService _loggingService;

        public Worker(ILogger<Worker> logger)
        {
            _client = new DiscordSocketClient();
            _commandService = new CommandService();
            _loggingService = new LoggingService(_client, _commandService, logger);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //TODO: move token to safer place
            var token = "MTE5ODc1NzI2ODQ4MTYzODU1MQ.GCNjdu.vZlEfS0GKw5-yESdMLV-RxbEf04I3FGVnwvKiE";

            //Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            //var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            //var token = File.ReadAllText("token.txt");
            //var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                // Block this task until the program is closed.
                await Task.Delay(-1);
            }

        }
    }
}