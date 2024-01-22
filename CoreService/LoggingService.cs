using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace CoreService
{
    public class LoggingService
    {
        private readonly ILogger<Worker> _logger;

        public LoggingService(DiscordSocketClient client, CommandService command, ILogger<Worker> logger)
        {
            _logger = logger;
            client.Log += LogAsync;
            command.Log += LogAsync;
        }

        private Task LogAsync(LogMessage message)
        {
            if (message.Exception is CommandException cmdException)
            {
                _logger.LogError($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
                    + $" failed to execute in {cmdException.Context.Channel}.");
                _logger.LogError(cmdException.ToString());
            }
            else
                _logger.LogInformation($"[General/{message.Severity}] {message}");

            return Task.CompletedTask;
        }
    }
}
