namespace SimpleCalculator.source.Operation
{
    public class Multiplication : IOperation
    {
        private double firstNumber;
        private double secondNumber;

        public Multiplication(double firstNumber, double secondNumber)
        {
            this.firstNumber = firstNumber;
            this.secondNumber = secondNumber;
        }
        public double Execute()
        {
            return firstNumber * secondNumber;
        }
    }
}