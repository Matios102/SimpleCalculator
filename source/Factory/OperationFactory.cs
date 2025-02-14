using SimpleCalculator.source.Operation;

namespace SimpleCalculator.source.Factory
{
    public class OperationFactory
    {
        public IOperation CreateOperation(List<double> values, string operation)
        {
            if(operation == "sqrt" && values.Count != 1)
            {
                throw new ArgumentException($"Operation sqrt requires exactly 1 value. {values.Count} values were passed.");
            }
            else if(values.Count != 2)
            {
                throw new ArgumentException($"Operation {operation} requires exactly 2 values. {values.Count} values were passed.");
            }

            switch (operation)
            {
                case "add":
                    return new Addition(values[0], values[1]);
                case "sub":
                    return new Subtraction(values[0], values[1]);
                case "mul":
                    return new Multiplication(values[0], values[1]);
                case "sqrt":
                    return new SquareRoot(values[0]);
                default:
                    throw new ArgumentException($"Operation {operation} is not supported.");
            }    
        }
    }
}