using System.Net.Mime;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramSenderApp.Models;

namespace TelegramSenderApp.Services;

public class TelegramService
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<TelegramService> _logger;

    public TelegramService(ITelegramBotClient botClient, ILogger<TelegramService> logger)
    {
        _botClient = botClient;
        _logger = logger;
    }
    
    public async Task<long> GetMyChatIdAsync(string username)
    {
        try
        {
            var updates = await _botClient.GetUpdatesAsync();
            foreach (var update in updates)
            {
                if (update.Message.Chat.Username == username)
                {
                    return update.Message.Chat.Id;
                }
            }
            return 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return 0;
        }
    }
    
    public async Task SendMessageAsync(MessageRequest request)
    {
        try
        {
            await _botClient.SendTextMessageAsync(request.ChatId, request.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
