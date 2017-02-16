namespace RuleService.Repository
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Models;

    public interface IRepository : IDisposable
    {
        DbSet<RuleVariable> RuleVariables { get; set; }

        DbSet<Rule> Rules { get; set; }

        Task<int> SaveChangesAsync();
    }
}
