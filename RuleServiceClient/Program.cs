namespace RuleServiceClient
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Default;
    using RuleService.Models;
    using RuleService.Models.Expressions;

    public static class Program
    {
        public static void Main(string[] args)
        {
            Uri serviceUri = new Uri("http://localhost:38054");
            var container = new Container(serviceUri);

            var firstRuleVariableId = CreateRuleVariable(container);
            var secondRuleVariableId = CreateRuleVariable(container);
            var thirdRuleVariableId = CreateRuleVariable(container);

            var expression = ExpressionBinaryOperation.CreateExpressionBinaryOperation(
                ExpressionBinaryOperator.And,
                ExpressionBinaryOperation.CreateExpressionBinaryOperation(
                    ExpressionBinaryOperator.Or,
                    ExpressionVariable.CreateExpressionVariable(firstRuleVariableId),
                    ExpressionVariable.CreateExpressionVariable(secondRuleVariableId)
                    ),
                ExpressionUnaryOperation.CreateExpressionUnaryOperation(
                    ExpressionUnaryOperator.Not,
                    ExpressionVariable.CreateExpressionVariable(thirdRuleVariableId)
                    )
                );

            var rule = Rule.CreateRule(int.MaxValue, "My Rule");
            rule.Expression = expression;
            container.AddToRules(rule);
            container.SaveChanges();
            Console.Out.WriteLine(rule.ToString());

            var ruleVariables = container.Rules.ByKey(rule.Id).Variables.ToList();
            var firstRuleVariable = ruleVariables.Find(rv => rv.Id == firstRuleVariableId);
            Console.Out.WriteLine(firstRuleVariable.ToString());
            var secondRuleVariable = ruleVariables.Find(rv => rv.Id == firstRuleVariableId);
            Console.Out.WriteLine(secondRuleVariable.ToString());
            var thirdRuleVariable = ruleVariables.Find(rv => rv.Id == firstRuleVariableId);
            Console.Out.WriteLine(thirdRuleVariable.ToString());
        }

        private static Rule GenerateRule()
        {
            var rule = Rule.CreateRule(0, "My Rule");
            return rule;
        }

        private static RuleVariable GenerateRuleVariable()
        {
            var ruleVariable = RuleVariable.CreateRuleVariable(0, "My Rule Variable", false);
            return ruleVariable;
        }

        private static int CreateRuleVariable(Container container)
        {
            var ruleVariable = CreateAndGetRuleVariable(container);
            return ruleVariable.Id;
        }

        private static RuleVariable CreateAndGetRuleVariable(Container container)
        {
            var ruleVariable = GenerateRuleVariable();
            container.AddToRuleVariables(ruleVariable);
            container.SaveChanges();
            return ruleVariable;
        }

        private static int CreateRule(Container container)
        {
            var rule = CreateAndGetRule(container);
            return rule.Id;
        }

        private static Expression CreateExpression(Container container)
        {
            var expression = ExpressionBinaryOperation.CreateExpressionBinaryOperation(
                ExpressionBinaryOperator.And,
                ExpressionBinaryOperation.CreateExpressionBinaryOperation(
                    ExpressionBinaryOperator.Or,
                    ExpressionVariable.CreateExpressionVariable(CreateRuleVariable(container)),
                    ExpressionVariable.CreateExpressionVariable(CreateRuleVariable(container))
                    ),
                ExpressionUnaryOperation.CreateExpressionUnaryOperation(
                    ExpressionUnaryOperator.Not,
                    ExpressionVariable.CreateExpressionVariable(CreateRuleVariable(container))
                    )
                );
            return expression;
        }

        private static Rule CreateAndGetRule(Container container)
        {
            var rule = GenerateRule();
            rule.Expression = CreateExpression(container);
            container.AddToRules(rule);
            container.SaveChanges();
            return rule;
        }
    }
}
