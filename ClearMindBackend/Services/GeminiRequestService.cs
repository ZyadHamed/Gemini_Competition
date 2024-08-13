using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Gemini_Competition.Services
{
    public class GeminiRequestService
    {
        public static string GeneratePayload(string text, string imageUrl = "")
        {
            if(imageUrl == "")
            {
                var payload = new
                {
                    contents = new
                    {
                        role = "USER",
                        parts = new object[] 
                        {
                            new {text = text}
                        }
                    },
                    generation_config = new
                    {
                        temperature = 0.4,
                        top_p = 1,
                        top_k = 32,
                        max_output_tokens = 2048
                    }
                };
                return JsonConvert.SerializeObject(payload);
            }
            else
            {
                var payload = new
                {
                    contents = new
                    {
                        role = "USER",
                        parts = new object[] 
                        {
                            new {text = text},
                            new {
                                file_data = new {
                                        mime_type = "image/png",
                                        file_uri = imageUrl
                                    }
                                }
                        }
                    },
                    generation_config = new
                    {
                        temperature = 0.4,
                        top_p = 1,
                        top_k = 32,
                        max_output_tokens = 2048
                    }
                };
                return JsonConvert.SerializeObject(payload);
            }
        }
        public async static Task<string> SendGeminiPrompt(string prompt, string imageUrl = "")
        {
            var handler = new HttpClientHandler();
            using HttpClient httpClient = new(handler);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string requestURL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key=AIzaSyACNEmvc2M39wf7zntIgwwvmH3h4Uv-YNg";
            string payload = GeneratePayload(prompt, imageUrl);
            HttpResponseMessage response = await httpClient.PostAsync(requestURL,
                new StringContent(payload, Encoding.UTF8, new MediaTypeWithQualityHeaderValue("application/json")));

            response.EnsureSuccessStatusCode();

            string responseContentJSONStringified = await response.Content.ReadAsStringAsync();
            string combinedString = "";
            try
            {
                var data = (JObject)JsonConvert.DeserializeObject(responseContentJSONStringified);
                var parts = data.First.First.First.First.First.First.First.First;
                foreach (JProperty part in parts)
                {
                    combinedString += part.First.Value<string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return combinedString;
        }
    }
}
