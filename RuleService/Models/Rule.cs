namespace RuleService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Expressions;

    public sealed class Rule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //[Required]
        //Expression Expression { get; set; }

        //ICollection<RuleVariable> Variable { get; set; }
    }
}