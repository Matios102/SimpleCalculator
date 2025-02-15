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

            FileService fileService = new FileService(outputFile);

            CalculatorService calculatorService = new CalculatorService(inputFile, errorFile, fileService);
            calculatorService.Calculate();
        }
    }
}