using System.Text.Json;
using SimpleCalculator.source.Models;
using SimpleCalculator.source.Services;
using SimpleCalculator.source.Utils;

namespace SimpleCalculator.Tests
{
    public class JsonConverterTest
    {
        private readonly string logFilePath;
        private readonly LoggerService loggerService;

        public JsonConverterTest()
        {
            logFilePath = Path.GetTempFileName();
            loggerService = new LoggerService(logFilePath);
        }

        [Fact]
        public void Deserialize_ValidJson_ReturnsOperationList()
        {
            // Arrange
            string json = @"
            {
                ""operation1"": { ""operator"": ""add"", ""value1"": 1, ""value2"": 2 },
                ""operation2"": { ""operator"": ""sub"", ""value1"": 5, ""value2"": 3 }
            }";
            var converter = new OperationModelConverter(loggerService);
            var options = new JsonSerializerOptions();
            options.Converters.Add(converter);

            // Act
            var result = JsonSerializer.Deserialize<List<OperationModel>>(json, options);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("operation1", result[0].ObjectName);
            Assert.Equal("add", result[0].Operator);
            Assert.Equal(new List<double> { 1, 2 }, result[0].Values);
            Assert.Equal("operation2", result[1].ObjectName);
            Assert.Equal("sub", result[1].Operator);
            Assert.Equal(new List<double> { 5, 3 }, result[1].Values);
        }

        [Fact]
        public void Deserialize_InvalidJson_LogsError()
        {
            // Arrange
            string json = @"
            {
                ""operation1"": { ""operator"": ""add"", ""value1"": ""invalid"" }
            }";
            var converter = new OperationModelConverter(loggerService);
            var options = new JsonSerializerOptions();
            options.Converters.Add(converter);

            // Act
            var result = JsonSerializer.Deserialize<List<OperationModel>>(json, options);

            // Assert
            Assert.IsType<List<OperationModel>>(result);
            Assert.Empty(result);

            // Verify the log file contains the error message
            var logContents = File.ReadAllText(logFilePath);
            Assert.Contains("Error processing object", logContents);
        }

        [Fact]
        public void Deserialize_MissingOperator_LogsError()
        {
            // Arrange
            string json = @"
            {
                ""operation1"": { ""value1"": 1, ""value2"": 2 }
            }";
            var converter = new OperationModelConverter(loggerService);
            var options = new JsonSerializerOptions();
            options.Converters.Add(converter);

            // Act
            var result = JsonSerializer.Deserialize<List<OperationModel>>(json, options);

            // Assert
            Assert.IsType<List<OperationModel>>(result);
            Assert.Empty(result);

            // Verify the log file contains the error message
            var logContents = File.ReadAllText(logFilePath);
            Assert.Contains("Error processing object", logContents);
        }

        [Fact]
        public void Deserialize_AdditionalField_LogsError()
        {
            // Arrange
            string json = @"
            {
                ""operation1"": { ""operator"": ""add"", ""value1"": 1, ""value2"": 2, ""value3"": 3 }
            }";
            var converter = new OperationModelConverter(loggerService);
            var options = new JsonSerializerOptions();
            options.Converters.Add(converter);

            // Act
            var result = JsonSerializer.Deserialize<List<OperationModel>>(json, options);

            // Assert
            Assert.IsType<List<OperationModel>>(result);
            Assert.Empty(result);

            // Verify the log file contains the error message
            var logContents = File.ReadAllText(logFilePath);
            Assert.Contains("Error processing object", logContents);
        }
    }
}