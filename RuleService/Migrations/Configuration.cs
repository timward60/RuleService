namespace RuleService.Migrations
{
    using System.Data.Entity.Migrations;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<RuleService.Models.RuleServiceDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RuleServiceDataContext context)
        {
            context.RuleVariables.AddOrUpdate(
                rv => rv.Id,
                new RuleVariable { Id = 1, Name = "A", State = true },
                new RuleVariable { Id = 2, Name = "B", State = true },
                new RuleVariable { Id = 3, Name = "C", State = true } 
                );

            context.Rules.AddOrUpdate(
                r => r.Id,
                new Rule { Id = 4, Name = "Sample" }
            );
        }
    }
}
