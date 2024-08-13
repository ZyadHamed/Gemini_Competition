using Google.Cloud.Firestore;

namespace Gemini_Competition.Data
{
    [FirestoreData]
    public class Transcription
    {
        [FirestoreProperty]
        public Timestamp Timestamp { get; set; }
        [FirestoreProperty]
        public string AppName { get; set; }
        [FirestoreProperty]
        public List<TranscribedTask> Tasks { get; set; }
    }
}
