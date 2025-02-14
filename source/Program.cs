using SimpleCalculator.source.Services;

namespace SimpleCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string inputFile = "./source/input.json";
            string outputFile = "./source/output.txt";

            FileService fileService = new FileService(outputFile);

            CalculatorService calculatorService = new CalculatorService(inputFile, fileService);
            calculatorService.Calculate();
        }
    }
}