using System.Text.Json;
using System.Text.Json.Serialization;
using SimpleCalculator.source.Models;
using SimpleCalculator.source.Services;

namespace SimpleCalculator.source.utils
{
    // Converts JSON data to a list of OperationModel objects
    public class OperationModelConverter : JsonConverter<List<OperationModel>>
    {
        LoggerSerivce loggerSerivce;

        public OperationModelConverter(string errorFile)
        {
            loggerSerivce = new LoggerSerivce(errorFile);
        }
        public override List<OperationModel> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var operations = new List<OperationModel>();

            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                foreach (var element in doc.RootElement.EnumerateObject())
                {
                    try
                    {
                        var operation = ParseOperation(element);
                        operations.Add(operation);
                    }
                    catch (Exception ex)
                    {
                        loggerSerivce.LogError($"Error processing object '{element.Name}': {ex.Message}");
                    }
                }
            }

            return operations;
        }

        private OperationModel ParseOperation(JsonProperty element)
        {
            var operation = new OperationModel
            {
                ObjectName = element.Name,
                Operator = element.Value.GetProperty("operator").GetString() ?? throw new ArgumentException($"Operator missing in '{element.Name}'."),
                Values = new List<double>()
            };

            if (element.Value.TryGetProperty("value1", out JsonElement value1))
            {
                operation.Values.Add(ParseDouble(value1, "value1", element.Name));
            }

            if (element.Value.TryGetProperty("value2", out JsonElement value2))
            {
                operation.Values.Add(ParseDouble(value2, "value2", element.Name));
            }

            if (operation.Values.Count < 1 || operation.Values.Count > 2)
            {
                throw new ArgumentException($"Object must have 1 or 2 values.");
            }

            return operation;
        }

        private double ParseDouble(JsonElement valueElement, string valueName, string objectName)
        {
            if (!valueElement.TryGetDouble(out double result))
            {
                throw new ArgumentException($"{valueName} in object '{objectName}' is not a valid number.");
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, List<OperationModel> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
