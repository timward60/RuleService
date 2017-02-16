namespace RuleService.Repository.Fake
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public sealed class FakeRuleVariableDbSet : FakeDbSet<RuleVariable>
    {
        private int _ruleVariableIdCounter = 0;
        private Queue<int> _availableRuleVariableIds = new Queue<int>();

        public override RuleVariable Find(params object[] keyValues)
        {
            var id = (int)keyValues.Single();
            return this.SingleOrDefault(s => s.Id == id);
        }

        public override Task<RuleVariable> FindAsync(params object[] keyValues)
        {
            var rule = Find(keyValues);
            return Task.FromResult(rule);
        }

        public override Task<RuleVariable> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return FindAsync(keyValues);
        }

        public override RuleVariable Add(RuleVariable item)
        {
            if (_availableRuleVariableIds.Count > 0)
            {
                item.Id = _availableRuleVariableIds.Dequeue();
            }
            else
            {
                item.Id = _ruleVariableIdCounter;
                _ruleVariableIdCounter++;
            }

            return base.Add(item);
        }

        public override RuleVariable Remove(RuleVariable item)
        {
            var deletedItem = base.Remove(item);
            _availableRuleVariableIds.Enqueue(deletedItem.Id);
            return deletedItem;
        }
    }
}
