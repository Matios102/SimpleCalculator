using System.Text.Json;
using System.Text.Json.Serialization;
using SimpleCalculator.source.Models;
using SimpleCalculator.source.Services;

namespace SimpleCalculator.source.Utils
{
    // Converts JSON data to a list of OperationModel objects
    public class OperationModelConverter : JsonConverter<List<OperationModel>>
    {
        private LoggerService loggerService;

        public OperationModelConverter(LoggerService loggerService)
        {
            this.loggerService = loggerService;
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
                        loggerService.LogError($"Error processing object '{element.Name}': {ex.Message}");
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

            HashSet<string> validFields = new HashSet<string> { "operator", "value1", "value2" };
            
            foreach (var property in element.Value.EnumerateObject())
            {
                if (!validFields.Contains(property.Name))
                {
                    throw new ArgumentException($"Unexpected field '{property.Name}'. Allowed fields: 'operator', 'value1', and optionally 'value2'.");
                }
            }

            if (!element.Value.TryGetProperty("value1", out JsonElement value1))
            {
                throw new ArgumentException("Missing filed 'value1'.");
            }
            operation.Values.Add(ParseDouble(value1, "value1"));

            if (element.Value.TryGetProperty("value2", out JsonElement value2))
            {
                operation.Values.Add(ParseDouble(value2, "value2"));
            }

            return operation;
        }

        private double ParseDouble(JsonElement valueElement, string valueName)
        {
            if (!valueElement.TryGetDouble(out double result))
            {
                throw new ArgumentException($"{valueName} is not a valid number.");
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, List<OperationModel> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
