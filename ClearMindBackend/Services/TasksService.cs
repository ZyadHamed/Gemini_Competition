using Gemini_Competition.Data;

namespace Gemini_Competition.Services
{
    public class TasksService : ITasksService
    {
        public async Task<List<TranscribedTask>> GetTasks()
        {
            List<TranscribedTask> results = new List<TranscribedTask>();
            EmailsService emailsService = new EmailsService();
            Dictionary<string, string> emails = emailsService.GetEmails();
            foreach (KeyValuePair<string, string> kvp in emails)
            {
                string GeminiPrompt = "Analyze the following block of text for explicit tasks that require specific actions. Only identify tasks if they include a clear action directive (e.g., 'complete', 'submit', 'review', 'update') that directly instructs the reader to perform a specific task. If there are no such tasks, output 'No'. If tasks are found, list each task in the following format: 'Task Title; Task Description \\n Task Title; Task Description \\n etc.' The task title must be a good summary of the task and NOT just 'Task Title'. Ensure that only genuine tasks with clear actions are listed. Here is the text:"
                    + kvp.Value;
                string resultString = await GeminiRequestService.SendGeminiPrompt(GeminiPrompt);
                if (resultString != "No \n")
                {
                    foreach (string line in resultString.Split("\n"))
                    {
                        if (line != "")
                        {
                            try
                            {
                                int semiColonIndex = line.IndexOf(";");
                                if (semiColonIndex != -1)
                                {
                                    string taskTitle = line.Substring(0, semiColonIndex);
                                    string taskDescrption = line.Substring(semiColonIndex + 1);
                                    results.Add(new TranscribedTask
                                    {
                                        Title = taskTitle,
                                        Description = taskDescrption,
                                        IsCompleted = false
                                    });
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                        }
                    }
                }
            }
            return results;
        }
    }
}
