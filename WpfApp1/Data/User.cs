
namespace ClearMindWindows.Data
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Transcription> Transcriptions { get; set; }

    }
}
