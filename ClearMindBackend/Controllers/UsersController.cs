using Gemini_Competition.Data;
using Gemini_Competition.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gemini_Competition.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IFireStoreService _storeService;
        public UsersController(IFireStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<string>> AddUser(User user)
        {
            string result = await _storeService.AddUserAsync(user);
            if(result == "UserExistsError")
            {
                return Conflict("This user already exists in the database record. Please try again!");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<User> GetUser(string userEmail)
        {
            User result = await _storeService.GetUserAsync(userEmail);
            return result;
        }

        [HttpPut]
        [Route("")]
        public Dictionary<string, string> PutUser()
        {
            EmailsService emails = new EmailsService();
            return emails.GetEmails();
        }

        [HttpGet]
        [Route("login")]
        public Task<bool> CheckLogin(string email, string password)
        {
            return _storeService.CheckLoginExists(email, password);
        }
    }
}
