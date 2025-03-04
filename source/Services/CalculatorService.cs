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
        private LoggerService loggerService;
        public CalculatorService(string inputFile, FileService fileService, LoggerService loggerService)
        {
            this.inputFile = inputFile;
            this.fileService = fileService;
            this.loggerService = loggerService;
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
                            loggerService.LogError($"Error processing object '{operation.ObjectName}': Invalid operator '{operation.Operator}'.");
                        }
                    }
                    catch (Exception ex)
                    {
                        loggerService.LogError($"Error processing object '{operation.ObjectName}': {ex.Message}");
                    }
                }

                data.Sort((x, y) => x.Item2.CompareTo(y.Item2));

                fileService.SerializeToFile(data);
            }
            catch (Exception ex)
            {
                loggerService.LogError($"An error occurred: {ex.Message}");
            }
        }

        private List<OperationModel>? DeserializeJson()
        {
            try
            {
                string json = File.ReadAllText(inputFile);
                var options = new JsonSerializerOptions();
                options.Converters.Add(new OperationModelConverter(loggerService));

                List<OperationModel>? operationList = JsonSerializer.Deserialize<List<OperationModel>>(json, options);

                return operationList;
            }
            catch (Exception ex)
            {
                loggerService.LogError($"Error deserializing JSON: {ex.Message}");
                return null;
            }
        }
    }
}