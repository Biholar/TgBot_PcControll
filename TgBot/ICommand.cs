using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot;

public interface ICommand
{
    Task<Message> Execute(ITelegramBotClient client, long chatId = 470696076);
}