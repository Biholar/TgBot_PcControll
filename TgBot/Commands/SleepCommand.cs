using System.Runtime.InteropServices;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot;

public class SleepCommand:ICommand
{
    [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
    
    public async Task<Message>  Execute(ITelegramBotClient client,long chatId = 470696076)
    {
        var msg =  await client.SendTextMessageAsync(
            chatId: chatId,
            text: "Command has sent Sleep");
        Thread.Sleep(3000);
        SetSuspendState(false, true, true);
        return msg;
    }
}