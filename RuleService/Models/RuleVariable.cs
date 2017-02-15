namespace RuleService.Models
{
    using System.ComponentModel.DataAnnotations;

    public sealed class RuleVariable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool State { get; set; } = false;
    }
}