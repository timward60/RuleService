namespace RuleService.Models.Expressions
{
    using System.ComponentModel.DataAnnotations;

    public enum ExpressionBinaryOperator
    {
        Or,
        And
    }

    public sealed class ExpressionBinaryOperation : Expression
    {
        [Required]
        public ExpressionBinaryOperator Operator { get; set; }

        [Required]
        public Expression FirstOperand { get; set; }

        [Required]
        public Expression SecondOperand { get; set; }
    }
}