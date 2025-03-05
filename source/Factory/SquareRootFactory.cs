using SimpleCalculator.source.Operation;

namespace SimpleCalculator.source.Factory
{
    public class SquareRootFactory : IFactory
    {
        public IOperation Create(List<double> values)
        {
            if (values.Count != 1)
            {
                throw new ArgumentException($"Square Root operation requires exactly 1 value. {values.Count} values were passed.");
            }
            if (values[0] < 0)
            {
                throw new ArgumentException($"Square Root operation requires a non-negative value. {values[0]} was passed.");
            }

            return new SquareRoot(values[0]);
        }
    }
}