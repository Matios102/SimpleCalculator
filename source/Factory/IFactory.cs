using SimpleCalculator.source.Operation;

namespace SimpleCalculator.source.Factory
{
    public interface IFactory
    {
        IOperation  CreateOperation(List<double> values);
    }
}