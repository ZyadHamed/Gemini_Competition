using ClearMindWindows.Controls;
using ClearMindWindows.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClearMindWindows.RequestModels
{
    public class TasksModel
    {
        public async Task<List<TaskContainer>> GetTasks()
        {
            /*
            HttpClient httpClient = new HttpClient();
            string requestURL = "https://localhost:7232/Tasks" + "?userEmail=" + Properties.Settings.Default.Email;
            HttpResponseMessage response = await httpClient.GetAsync(requestURL);
            response.EnsureSuccessStatusCode();
            string resultResponse = await response.Content.ReadAsStringAsync();
            List<Transcription> transcriptions = JsonConvert.DeserializeObject<List<Transcription>>(resultResponse);
            */
            DateTime dt1 = new DateTime(2024, 8, 11, 5, 17, 42, 23);
            DateTimeOffset dto1 = new DateTimeOffset(dt1);
            DateTime dt2= new DateTime(2024, 8, 9, 5, 43, 21, 15);
            DateTimeOffset dto2 = new DateTimeOffset(dt2);
            List<Transcription> transcriptions = new List<Transcription>()
            {
                new Transcription(){
                    AppName = "Gmail",
                    Tasks = new List<TranscribedTask>()
                    {
                        new TranscribedTask()
                        {
                            Title = "Submit Kaggle Notebook Now",
                            Description = "Please submit your kaggle notebook for competition ISIC2024",
                            Timestamp = dto1.ToUnixTimeSeconds().ToString()
                        },
                        new TranscribedTask()
                        {
                            Title = "Arrange meeting with Zyad",
                            Description = "Make sure to arrange session 12 with Zyad",
                            Timestamp =  dto2.ToUnixTimeSeconds().ToString()
                        },
                    }
                }
            };
            List<TaskContainer> tasks = new List<TaskContainer>();
            foreach(Transcription transcription in transcriptions)
            {
                foreach(TranscribedTask task in transcription.Tasks)
                {
                    tasks.Add(new TaskContainer(task));
                }
            }
            return tasks;
        }

        public async Task SyncTasks()
        {
            HttpClient httpClient = new HttpClient();
            string requestURL = "https://localhost:7232/Tasks" + "?userEmail=" + Properties.Settings.Default.Email;
            HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7232/Tasks", new StringContent(Properties.Settings.Default.Email));
            response.EnsureSuccessStatusCode();
            string resultResponse = await response.Content.ReadAsStringAsync();
        }
    }
}
