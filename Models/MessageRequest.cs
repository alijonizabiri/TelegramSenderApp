namespace TelegramSenderApp.Models;

public class MessageRequest
{
    public long ChatId { get; set; }
    public string Message { get; set; }
    public string UserName { get; set; }
}
