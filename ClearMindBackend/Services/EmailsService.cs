using System.IO;
using System.Diagnostics;
namespace Gemini_Competition.Services
{
    public class EmailsService
    {
        string EmailsFilePath = @"..\ClearMindBackend\ExternalAssets\";
        public EmailsService()
        {
            RunScript("emailscript.py");
        }
        public Dictionary<string, string> GetEmails()
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            foreach (string file in Directory.EnumerateFiles(EmailsFilePath))
            {
                FileInfo info = new FileInfo(file);
                if (info.Extension == ".txt")
                {
                    string rawName = info.Name.Substring(0, info.Name.LastIndexOf("."));
                    string text = GetEmailText(rawName);
                    if (text != "")
                    {
                        results.Add(rawName, text);
                    }
                }
            }
            return results;
        }
        public string GetEmailText(string emailId)
        {
            string data = File.ReadAllText(EmailsFilePath + emailId + ".txt");
            return data;
        }
        void RunScript(string scriptName)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(scriptName)
            {
                UseShellExecute = true,
                WorkingDirectory = EmailsFilePath
            };
            p.Start();
            p.WaitForExit();
        }
    }
}
