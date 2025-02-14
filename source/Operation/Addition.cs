namespace SimpleCalculator.source.Operation
{
    public class Addition : IOperation
    {
        private double firstNumber;
        private double secondNumber;

        public Addition(double firstNumber, double secondNumber)
        {
            this.firstNumber = firstNumber;
            this.secondNumber = secondNumber;
        }

        public double Execute()
        {
            return firstNumber + secondNumber;
        }
    }
}