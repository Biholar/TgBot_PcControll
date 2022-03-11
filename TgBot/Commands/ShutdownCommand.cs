using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot;

public class ShutdownCommand:ICommand
{
    public async Task<Message> Execute(ITelegramBotClient client,long chatId = 470696076)
    {
        
        var msg =  await client.SendTextMessageAsync(
            chatId: chatId,
            text: "Command has sent Shutdown");
        Process.Start("shutdown","/s /t 3");
        return msg;
    }
}