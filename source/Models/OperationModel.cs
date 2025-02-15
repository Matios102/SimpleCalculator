namespace SimpleCalculator.source.Models
{
    public class OperationModel
    {
        public required string ObjectName {get; set;}
        public required string Operator { get; set; }
        public required List<double> Values { get; set; }
    }
}