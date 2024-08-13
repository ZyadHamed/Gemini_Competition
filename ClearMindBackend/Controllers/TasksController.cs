using Gemini_Competition.Data;
using Gemini_Competition.Services;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;

namespace Gemini_Competition.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        IFireStoreService _fireStoreService;
        ITasksService _taskService;
        public TasksController(IFireStoreService fireStoreService, ITasksService tasksService)
        {
            _fireStoreService = fireStoreService;
            _taskService = tasksService;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddNewTasks(string userEmail)
        {
            List<TranscribedTask> tasks = await _taskService.GetTasks();
            Google.Cloud.Firestore.Timestamp currentTimestamp = Google.Cloud.Firestore.Timestamp.FromDateTime(DateTime.UtcNow);
            Transcription transcription = new Transcription()
            {
                AppName = "Gmail",
                Timestamp = currentTimestamp,
                Tasks = tasks
            };
            bool result = await _fireStoreService.AddNewTranscription(userEmail, transcription);
            return Ok(result);
        }
        [HttpGet]
        public async Task<List<Transcription>> GetTranscriptions(string userEmail)
        {
            User user = await _fireStoreService.GetUserAsync(userEmail);
            return user.Transcriptions;
        }
    }
}
