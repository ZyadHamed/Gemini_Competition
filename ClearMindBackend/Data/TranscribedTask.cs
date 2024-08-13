using Google.Cloud.Firestore;

namespace Gemini_Competition.Data
{
    [FirestoreData]
    public class TranscribedTask
    {
        [FirestoreProperty]
        public string Title { get; set; }
        [FirestoreProperty]
        public string Description { get; set; }
        [FirestoreProperty]
        public Timestamp Deadline { get; set; }
        [FirestoreProperty]
        public bool IsCompleted { get; set; }
    }
}
