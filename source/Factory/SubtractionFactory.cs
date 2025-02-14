using SimpleCalculator.source.Operation;

namespace SimpleCalculator.source.Factory
{
    public class SubtractionFactory : IFactory
    {
        public IOperation CreateOperation(List<double> values)
        {
            if(values.Count != 2) 
            {
                throw new ArgumentException($"Subtraction operation requires exactly 2 values. {values.Count} values were passed.");
            }

            return new Subtraction(values[0], values[1]);
        }
    }
}