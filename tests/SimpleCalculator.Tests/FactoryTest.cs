using SimpleCalculator.source.Factory;
using SimpleCalculator.source.Operation;

namespace SimpleCalculator.Tests
{
    public class FactoryTest
    {
        [Fact]
        public void AdditionFactory_Create_ReturnsAdditionOperation()
        {
            // Arrange
            var factory = new AdditionFactory();

            // Act
            var values = new List<double> {2, 3};
            var operation = factory.Create(values);

            // Assert
            Assert.IsType<Addition>(operation);
            Assert.Equal(5, operation.Execute());
        }

        [Fact]
        public void AdditionFactory_Create_ThrowsArgumentException()
        {
            // Arrange
            var factory = new AdditionFactory();

            // Act
            var values = new List<double> {2, 3, 4};

            // Assert
            Assert.Throws<ArgumentException>(() => factory.Create(values));
        }

        [Fact]
        public void MultiplicationFactory_Create_ReturnsMultiplicationOperation()
        {
            // Arrange
            var factory = new MultiplicationFactory();

            // Act
            var values = new List<double> {2, 3};
            var operation = factory.Create(values);

            // Assert
            Assert.IsType<Multiplication>(operation);
            Assert.Equal(6, operation.Execute());
        }

        [Fact]
        public void MultiplicationFactory_Create_ThrowsArgumentException()
        {
            // Arrange
            var factory = new MultiplicationFactory();

            // Act
            var values = new List<double> {2};

            // Assert
            Assert.Throws<ArgumentException>(() => factory.Create(values));
        }

        [Fact]
        public void SquareRootFactory_Create_ReturnsSquareRootOperation()
        {
            // Arrange
            var factory = new SquareRootFactory();

            // Act
            var values = new List<double> {9};
            var operation = factory.Create(values);

            // Assert
            Assert.IsType<SquareRoot>(operation);
            Assert.Equal(3, operation.Execute());
        }

        [Fact]
        public void SquareRootFactory_Create_ThrowsArgumentException()
        {
            // Arrange
            var factory = new SquareRootFactory();

            // Act
            var values = new List<double> {9, 3};

            // Assert
            Assert.Throws<ArgumentException>(() => factory.Create(values));
        }

        [Fact]
        public void SubtractionFactory_Create_ReturnsSubtractionOperation()
        {
            // Arrange
            var factory = new SubtractionFactory();

            // Act
            var values = new List<double> {5, 3};
            var operation = factory.Create(values);

            // Assert
            Assert.IsType<Subtraction>(operation);
            Assert.Equal(2, operation.Execute());
        }

        [Fact]
        public void SubtractionFactory_Create_ThrowsArgumentException()
        {
            // Arrange
            var factory = new SubtractionFactory();

            // Act
            var values = new List<double> {5};

            // Assert
            Assert.Throws<ArgumentException>(() => factory.Create(values));
        }
    }
}