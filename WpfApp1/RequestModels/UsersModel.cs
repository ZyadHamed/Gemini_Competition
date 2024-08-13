using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ClearMindWindows.Data;
using System.Xml.Linq;

namespace ClearMindWindows.RequestModels
{
    public class UsersModel
    {
        string baseURL = "https://localhost:7232/Users";
        public async Task<string> AddUser(string Name, string Email, string Password)
        {
            HttpClient httpClient = new HttpClient();
            dynamic userSchema = new
            {
                Id = "",
                Name = Name,
                Email = Email,
                Password = Password,
                Transcriptions = new List<Transcription>()
            };
            var jsonSchema = JsonConvert.SerializeObject(userSchema);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.PostAsync(baseURL,
                new StringContent(jsonSchema, Encoding.UTF8, new MediaTypeWithQualityHeaderValue("application/json")));
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            if(content == "")
            {
                return "Your account has been created successfully!";
            }
            return content;
        }
        public async Task<bool> CheckUserLogin(string Email, string Password)
        {
            HttpClient httpClient = new HttpClient();
            string requestURL = baseURL + "/login?email=" + Email + "&password=" + Password;
            HttpResponseMessage response = await httpClient.GetAsync(requestURL);
            response.EnsureSuccessStatusCode();
            string resultResponse = await response.Content.ReadAsStringAsync();
            if(resultResponse == "true")
            {
                return true;
            }
            return false;
        }

        public async Task<User> GetUser(string Email)
        {
            HttpClient httpClient = new HttpClient();
            string requestURL = baseURL + "?userEmail=" + Email;
            HttpResponseMessage response = await httpClient.GetAsync(requestURL);
            response.EnsureSuccessStatusCode();
            string resultResponse = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(resultResponse);
            return user;
        }
    }
}
