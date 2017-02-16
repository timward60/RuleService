namespace RuleService.Models.Expressions
{
    using System.ComponentModel.DataAnnotations;

    public sealed class ExpressionVariable : Expression
    {
        [Required]
        public int RuleVariableId { get; set; }
    }
}