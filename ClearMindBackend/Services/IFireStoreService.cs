using Gemini_Competition.Data;

namespace Gemini_Competition.Services
{
    public interface IFireStoreService
    {
        Task<string> AddUserAsync(User user);
        Task<User> GetUserAsync(string userEmail);
        Task<string> DeleteUserAsync(string userEmail);
        Task<bool> AddNewTranscription(string useremail, Transcription transcription);
        Task<bool> CheckLoginExists(string email, string password);
    }
}
