namespace SimpleCalculator.source.Operation
{
    public class SquareRoot : IOperation
    {
        private double number;

        public SquareRoot(double number)
        {
            this.number = number;
        }
        public double Execute()
        {
            return Math.Sqrt(number);
        }
    }
}