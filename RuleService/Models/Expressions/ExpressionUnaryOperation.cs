namespace RuleService.Models.Expressions
{
    using System.ComponentModel.DataAnnotations;

    public enum ExpressionUnaryOperator
    {
        Not
    }

    public sealed class ExpressionUnaryOperation : Expression
    {
        [Required]
        public ExpressionUnaryOperator Operator { get; set; }

        [Required]
        public Expression Operand { get; set; }
    }
}