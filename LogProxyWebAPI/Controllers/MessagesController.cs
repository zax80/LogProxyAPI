using LogProxyWebAPI.Authentication;
using LogProxyWebAPI.Models;
using LogProxyWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogProxyWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogProxyService _logProxyService;

        public MessagesController(ILogProxyService logProxyService)
        {
            _logProxyService = logProxyService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _logProxyService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPost("addmessage")]
        public async Task<IActionResult> AddMessage([FromBody] MessageModel model)
        {
            var response = await _logProxyService.AddMessage(model.Title, model.Text);

            if (response == null)
                return BadRequest(new { message = "Sorry, could not add the message" });

            return Ok(response);
        }

        [HttpGet("getallmessages")]
        public async Task<IActionResult> GetAllMessages()
        {
            var messages = await _logProxyService.GetAllMessages();
            return Ok(messages);
        }
    }
}