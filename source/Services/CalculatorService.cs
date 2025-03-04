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
        private string errorFile;
        private FileService fileService;
        private bool errorLogged = false;
        private LoggerSerivce loggerSerivce;
        public CalculatorService(string inputFile, string errorFile, FileService fileService)
        {
            this.inputFile = inputFile;
            this.fileService = fileService;
            this.errorFile = errorFile;
            loggerSerivce = new LoggerSerivce(errorFile);
        }

        public void Calculate()
        {
            try
            {
                List<OperationModel>? operationList = DeserializeJson();

                var data = new List<Tuple<string, double>>();

                if (operationList == null)
                {
                    Console.WriteLine("No operations to process.");
                    return;
                }

                foreach (var operation in operationList)
                {
                    try
                    {
                        FactoryDictionaryProvider.FactoryDictionary.TryGetValue(operation.Operator, out IFactory? factory);
                        if (factory != null)
                        {
                            IOperation operationHandler = factory.CreateOperation(operation.Values);
                            data.Add(Tuple.Create(operation.ObjectName, operationHandler.Execute()));
                        }
                        else
                        {
                            loggerSerivce.LogError($"Factory not found for operator '{operation.Operator}'");
                            if (!errorLogged)
                            {
                                Console.WriteLine($"Errors have been logged to {errorFile}");
                                errorLogged = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        loggerSerivce.LogError($"Error processing operation '{operation.ObjectName}': {ex.Message}");
                        if (!errorLogged)
                        {
                            Console.WriteLine($"Errors have been logged to {errorFile}");
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
                    Console.WriteLine($"Errors have been logged to {errorFile}");
                    errorLogged = true;
                }
            }
        }

        private List<OperationModel>? DeserializeJson()
        {
            string json = File.ReadAllText(inputFile);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new OperationModelConverter(errorFile));

            List<OperationModel>? operationList = JsonSerializer.Deserialize<List<OperationModel>>(json, options);

            return operationList;
        }
    }
}