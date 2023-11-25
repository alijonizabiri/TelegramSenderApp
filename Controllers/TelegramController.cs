using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramSenderApp.Models;
using TelegramSenderApp.Services;

namespace TelegramSenderApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TelegramController : ControllerBase
{
    private readonly TelegramService _service;

    public TelegramController(TelegramService service)
    {
        _service = service;
    }

    [HttpPost("send-message")]
    public async Task<IActionResult> SendMessage([FromBody] MessageRequest request)
    {
        if (request.ChatId == 0)
        {
            request.ChatId = await _service.GetMyChatIdAsync(request.UserName);
        }
        await _service.SendMessageAsync(request);

        return Ok("Message sent successfully");
    }
}

