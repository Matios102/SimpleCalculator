using SimpleCalculator.source.Operation;

namespace SimpleCalculator.source.Factory
{
    public class MultiplicationFactory : IFactory
    {
        public IOperation Create(List<double> values)
        {
            if (values.Count != 2)
            {
                throw new ArgumentException($"Multiplication operation requires exactly 2 values. {values.Count} values were passed.");
            }

            return new Multiplication(values[0], values[1]);
        }
    }
}