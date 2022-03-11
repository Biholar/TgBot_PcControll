using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ITelegramBotClient _client;

    public Worker(ILogger<Worker> logger, ITelegramBotClient telegramBotClient)
    {
        _logger = logger;
        _client = telegramBotClient;
    }

    void StartBot()
    {
        _client.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions{ AllowedUpdates = {}},
            cancellationToken: new CancellationTokenSource().Token);
    }

    private Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }

    async Task<Message> ExecuteCommand(ITelegramBotClient client, ICommand command, long chatId = 470696076)
    {
       return await command.Execute(client);
       }
    
    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken Token)
    {
        if (update.Type != UpdateType.Message)
            return;
        // Only process text messages
        if (update.Message!.Type != MessageType.Text)
            return;
        if(update.Message.Chat.Id != 470696076)
            return;
        var chatId = update.Message.Chat.Id;
        var messageText = update.Message.Text;
        
        Message sentMessage = messageText switch
        {
            "/start" =>await ExecuteCommand (client, new StartCommand()),
            "/shutdown" =>await ExecuteCommand(client,new ShutdownCommand()),
            "/sleep" =>await ExecuteCommand(client, new SleepCommand()),
            _=> new Message()
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        StartBot();
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(36000, stoppingToken);
        }
    }
}