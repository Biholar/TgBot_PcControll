using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot;

public class StartCommand:ICommand
{
    async public Task<Message> Execute(ITelegramBotClient client,long chatId = 470696076)
    { 
        return await client.SendTextMessageAsync(
        chatId: chatId,
        text: "Command has sent Hello");
    }
}