using SimpleCalculator.source.Operation;

namespace SimpleCalculator.Tests
{
    public class OperationTest
    {
        [Fact]
        public void TestAddition()
        {
            // Arrange
            var addition = new Addition(2, 3);

            // Act
            var result = addition.Execute();

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void TestSubtraction()
        {
            // Arrange
            var subtraction = new Subtraction(5, 3);

            // Act
            var result = subtraction.Execute();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void TestMultiplication()
        {
            // Arrange
            var multiplication = new Multiplication(2, 3);

            // Act
            var result = multiplication.Execute();

            // Assert
            Assert.Equal(6, result);
        }

        [Fact]
        public void TestSquareRoot()
        {
            // Arrange
            var squareRoot = new SquareRoot(9);

            // Act
            var result = squareRoot.Execute();

            // Assert
            Assert.Equal(3, result);
        }
    }
}