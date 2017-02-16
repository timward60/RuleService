namespace RuleService.Repository.Fake
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public sealed class FakeRuleDbSet : FakeDbSet<Rule>
    {
        private int _ruleIdCounter = 0;
        private Queue<int> _availableRuleIds = new Queue<int>();

        public override Rule Find(params object[] keyValues)
        {
            var id = (int)keyValues.Single();
            return this.SingleOrDefault(r => r.Id == id);
        }

        public override Task<Rule> FindAsync(params object[] keyValues)
        {
            var rule = Find(keyValues);
            return Task.FromResult(rule);
        }

        public override Task<Rule> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return FindAsync(keyValues);
        }

        public override Rule Add(Rule item)
        {
            if (_availableRuleIds.Count > 0)
            {
                item.Id = _availableRuleIds.Dequeue();
            }
            else
            {
                item.Id = _ruleIdCounter;
                _ruleIdCounter++;
            }
            
            if (item.Variables == null)
            {
                item.Variables = new List<RuleVariable>();
            }
            
            return base.Add(item);
        }

        public override Rule Remove(Rule item)
        {
            var deletedItem = base.Remove(item);
            _availableRuleIds.Enqueue(deletedItem.Id);
            return deletedItem;
        }
    }
}