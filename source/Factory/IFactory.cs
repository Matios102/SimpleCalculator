using SimpleCalculator.source.Operation;

namespace SimpleCalculator.source.Factory
{
    public interface IFactory
    {
        IOperation  Create(List<double> values);
    }
}