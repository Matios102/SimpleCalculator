namespace SimpleCalculator.source.Services
{
    public class LoggerService
    {
        private string logFile;
        private bool errorLogged = false;

        public LoggerService(string logFile)
        {
            this.logFile = logFile;
        }

        public void LogError(string message)
        {
            if (!errorLogged)
            {
                Console.WriteLine($"Errors have been logged to {logFile}");
                errorLogged = true;
            }
            using (StreamWriter writer = new StreamWriter(logFile, append: true))
            {
                writer.WriteLine("========================================");
                writer.WriteLine($"Timestamp: {DateTime.Now}");
                writer.WriteLine("Severity: ERROR");
                writer.WriteLine($"Message: {message}");
                writer.WriteLine("========================================");
                writer.WriteLine();
            }
        }
    }
}