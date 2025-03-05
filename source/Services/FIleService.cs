namespace SimpleCalculator.source.Services
{
    public class FileService
    {
        private string outputFile;

        public FileService(string outputFile)
        {
            this.outputFile = outputFile;
        }

        public void SerializeToFile(List<Tuple<string, double>> data)
        {
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (var item in data)
                {
                    writer.WriteLine($"{item.Item1}: {item.Item2}");
                }
            }
            Console.WriteLine($"Data serilaized to {outputFile}");
        }
    }
}