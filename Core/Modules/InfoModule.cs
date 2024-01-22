using Discord;
using Discord.Interactions;

namespace Core.Modules
{
    public class InfoModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("say", "Make the bot say something.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public Task Say(string text)
            => RespondAsync(text);

        [SlashCommand("ping", "Receive a ping message")]
        public async Task HandlePingCommand()
        {
            await RespondAsync("Pong!");
        }
    }
}