namespace RuleService.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Expressions;

    public sealed class Rule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Expression Expression { get; set; }

        public ICollection<RuleVariable> Variables { get; set; }
    }
}