namespace RuleService.Models
{
    using System.Data.Entity;

    public sealed class RuleServiceDataContext : DbContext
    {    
        public RuleServiceDataContext() : base("name=RuleServiceDataContext")
        {
        }

        public DbSet<RuleVariable> RuleVariables { get; set; }

        public DbSet<Rule> Rules { get; set; }
    }
}
