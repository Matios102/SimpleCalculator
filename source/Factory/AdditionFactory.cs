using SimpleCalculator.source.Operation;

namespace SimpleCalculator.source.Factory
{
    public class AdditionFactory : IFactory
    {
        public IOperation Create(List<double> values)
        {
            if (values.Count != 2)
            {
                throw new ArgumentException($"Addition operation requires exactly 2 values. {values.Count} values were passed.");
            }
            return new Addition(values[0], values[1]);
        }
    }
}