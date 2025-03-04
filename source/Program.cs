using SimpleCalculator.source.Services;

namespace SimpleCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string inputFile = "./source/input.json";
            string outputFile = "./source/output.txt";
            string errorFile = "./source/error.txt";

            LoggerService loggerService = new LoggerService(errorFile);
            FileService fileService = new FileService(outputFile);

            CalculatorService calculatorService = new CalculatorService(inputFile, fileService, loggerService);
            calculatorService.Calculate();
        }
    }
}