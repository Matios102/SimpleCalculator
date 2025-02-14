namespace SimpleCalculator.source.Operation
{
    public class Subtraction : IOperation
    {
        private double firstNumber;
        private double secondNumber;

        public Subtraction(double firstNumber, double secondNumber)
        {
            this.firstNumber = firstNumber;
            this.secondNumber = secondNumber;
        }

        public double Execute()
        {
            return firstNumber - secondNumber;
        }
    }
}