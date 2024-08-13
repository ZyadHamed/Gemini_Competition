namespace ClearMindWindows.Data
{
    public class Transcription
    {
        public string Timestamp { get; set; }
        public string AppName { get; set; }
        public List<TranscribedTask> Tasks { get; set; }
    }
}
