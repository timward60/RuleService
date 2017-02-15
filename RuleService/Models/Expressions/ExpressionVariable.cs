namespace RuleService.Models.Expressions
{
    using System.ComponentModel.DataAnnotations;

    public sealed class ExpressionVariable : Expression
    {
        [Required]
        int RuleVariableId { get; set; }
    }
}