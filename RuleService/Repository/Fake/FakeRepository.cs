namespace RuleService.Repository.Fake
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Models;

    public sealed class FakeRepository : IRepository
    {
        private static volatile FakeRepository _instance;
        private static object _lock = new object();

        static public FakeRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new FakeRepository();
                        }
                    }
                }
                return _instance;
            }
        }

        public int SaveChangesCount { get; private set; }

        public FakeRepository()
        {
            Rules = new FakeRuleDbSet();
            RuleVariables = new FakeRuleVariableDbSet();
        }

        public void Dispose()
        {

        }

        /// <inheritdoc />
        public DbSet<Rule> Rules { get; set; }

        /// <inheritdoc />
        public DbSet<RuleVariable> RuleVariables { get; set; }

        /// <inheritdoc />
        public Task<int> SaveChangesAsync()
        {
            SaveChangesCount++;
            return Task.FromResult(SaveChangesCount);
        }
    }
}