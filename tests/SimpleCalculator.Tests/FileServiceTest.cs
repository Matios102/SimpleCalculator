using SimpleCalculator.source.Services;

namespace SimpleCalculator.Tests
{
    public class FilesServiceTest
    {
        [Fact]
        public void ReadFile_ReturnsFileContent()
        {
            // Arrange
            var filePath = Path.GetTempFileName();
            var fileService = new FileService(filePath);
            var output = new List<Tuple<string, double>> {new Tuple<string, double>("obj", 5)};
            var expected = "obj: 5\n";
            
            // Act
            fileService.SerializeToFile(output);

            // Assert
            Assert.Equal(expected, File.ReadAllText(filePath));
        }
    }
}