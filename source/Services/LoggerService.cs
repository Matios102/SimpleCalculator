namespace SimpleCalculator.source.Services
{
    public class LoggerSerivce
    {
        private string logFile;

        public LoggerSerivce(string logFile)
        {
            this.logFile = logFile;
        }

        public void LogError(string message)
        {
            using (StreamWriter writer = new StreamWriter(logFile, append: true))
            {
                writer.WriteLine($"[{DateTime.Now}] ERROR: {message}");
            }
        }
    }
}