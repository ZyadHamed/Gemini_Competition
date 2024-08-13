using Gemini_Competition.Data;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Gemini_Competition.Services
{
    public class FireStoreService : IFireStoreService
    {
        private readonly FirestoreDb _firestoreDb;

        public FireStoreService(string projectID)
        {
            var jsonString = File.ReadAllText(@"../ClearMindBackend/firebasecredientials.json");
            FirestoreClientBuilder builder = new FirestoreClientBuilder { JsonCredentials = jsonString };
            _firestoreDb = FirestoreDb.Create(projectID, builder.Build());
        }
        public async Task<string> AddUserAsync(User user)
        {
            CollectionReference collection = _firestoreDb.Collection("Users");
            try
            {
                bool userExists = await CheckUserExists(user.Email);
                if (userExists)
                {
                    return "UserExistsError";
                }
                string hashedPassword = HashingService.GetHashString(user.Password);
                user.Password = hashedPassword;
                await collection.AddAsync(user);
                return user.Id;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        public Task<string> DeleteUserAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserAsync(string userEmail)
        {
            try
            {
                CollectionReference collection = _firestoreDb.Collection("Users");
                Query query = collection.WhereEqualTo("Email", userEmail);
                QuerySnapshot resultsSnapshot = await query.GetSnapshotAsync();
                if (resultsSnapshot.Count == 0)
                {
                    return null;
                }
                DocumentSnapshot resultSnapshot = resultsSnapshot.Documents[0];
                string id = resultSnapshot.Id;
                string name = resultSnapshot.GetValue<string>("Name");
                string password = resultSnapshot.GetValue<string>("Password");
                List<Transcription> transcriptions = null;
                resultSnapshot.TryGetValue<List<Transcription>>("Transcriptions", out transcriptions);
                return new User
                {
                    Id = id,
                    Name = name,
                    Email = userEmail,
                    Password = password,
                    Transcriptions = transcriptions
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> CheckUserExists(string userEmail)
        {
            CollectionReference collection = _firestoreDb.Collection("Users");
            Query query = collection.WhereEqualTo("Email", userEmail);
            QuerySnapshot resultsSnapshot = await query.GetSnapshotAsync();
            return resultsSnapshot.Count > 0;
        }

        public async Task<bool> CheckLoginExists(string userEmail, string password)
        {
            CollectionReference collection = _firestoreDb.Collection("Users");
            Query query = collection.WhereEqualTo("Email", userEmail);
            QuerySnapshot resultsSnapshot = await query.GetSnapshotAsync();
            if(resultsSnapshot.Count > 0)
            {
                DocumentSnapshot resultSnapshot = resultsSnapshot.Documents[0];
                string existingPassword = resultSnapshot.GetValue<string>("Password");
                return HashingService.CompareHash(password, existingPassword);
            }
            return false;
        }
        
        public async Task<bool> AddNewTranscription(string userEmail, Transcription transcription)
        {
            User user = await GetUserAsync(userEmail);
            if(user == null)
            {
                return false;
            }
            if(user.Transcriptions != null)
            {
                user.Transcriptions.Add(transcription);
            }
            else
            {
                user.Transcriptions = new List<Transcription>() { transcription };
            }
            DocumentReference docref = _firestoreDb.Collection("Users").Document(user.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "Transcriptions",user.Transcriptions},
            };
            Google.Cloud.Firestore.WriteResult result = await docref.UpdateAsync(data);
            return true;

        }
        private dynamic UserToDocument(User user)
        {
            return new
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Transcriptions = user.Transcriptions
            };
        }
    }
}
