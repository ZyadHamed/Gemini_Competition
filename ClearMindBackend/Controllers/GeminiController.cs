using Gemini_Competition.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gemini_Competition.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeminiController : Controller
    {
        [HttpPost]
        [Route("")]
        public async Task<string> GetResponse(string question)
        {
            return await GeminiRequestService.SendGeminiPrompt(question);
        }
    }
}
