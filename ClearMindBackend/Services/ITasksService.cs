using Gemini_Competition.Data;

namespace Gemini_Competition.Services
{
    public interface ITasksService
    {
        public Task<List<TranscribedTask>> GetTasks();
    }
}
