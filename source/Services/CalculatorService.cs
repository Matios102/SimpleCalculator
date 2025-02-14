using System.Text.Json;
using SimpleCalculator.source.Factory;
using SimpleCalculator.source.Models;
using SimpleCalculator.source.Operation;
using SimpleCalculator.source.utils;

namespace SimpleCalculator.source.Services
{
    public class CalculatorService
    {
        private string inputFile;
        private FileService fileService;
        private bool errorLogged = false;

        private LoggerSerivce loggerSerivce = new LoggerSerivce("./source/error_log.txt");
        public CalculatorService(string inputFile, FileService fileService)
        {
            this.inputFile = inputFile;
            this.fileService = fileService;
        }

        public void Calculate()
        {
            try
            {
                List<OperationModel>? operationList = DeserializeJson();

                var data = new List<Tuple<string, double>>();

                OperationFactory factory = new OperationFactory();

                if (operationList == null)
                {
                    Console.WriteLine("No operations to process.");
                    return;
                }

                foreach (var operation in operationList)
                {
                    try
                    {
                        IOperation operationHandler = factory.CreateOperation(operation.Values, operation.Operator);
                        data.Add(Tuple.Create(operation.ObjectName, operationHandler.Execute()));
                    }
                    catch (Exception ex)
                    {
                        loggerSerivce.LogError($"Error processing operation '{operation.ObjectName}': {ex.Message}");
                        if (!errorLogged)
                        {
                            Console.WriteLine("Errors have been logged to ./source/error_log.txt");
                            errorLogged = true;
                        }
                    }
                }

                data.Sort((x, y) => x.Item2.CompareTo(y.Item2));

                fileService.SerializeToFile(data);
            }
            catch (Exception ex)
            {
                loggerSerivce.LogError($"An error occurred: {ex.Message}");
                if (!errorLogged)
                {
                    Console.WriteLine("Errors have been logged to ./source/error_log.txt");
                    errorLogged = true;
                }
            }
        }

        private List<OperationModel>? DeserializeJson()
        {
            string json = File.ReadAllText(inputFile);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new OperationModelConverter());

            List<OperationModel>? operationList = JsonSerializer.Deserialize<List<OperationModel>>(json, options);

            return operationList;
        }
    }
}