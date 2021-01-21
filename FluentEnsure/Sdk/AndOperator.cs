using System;

namespace FluentEnsure.Sdk
{
    internal class AndOperator<T> : RuleCombinationBase<T>, IRealBeforeRule<T>
    {
        public AndOperator(IRealRule<T> previousRule) : base(previousRule)
        {
        }

        public override IRealRule<T> CombineWith(Func<Exception> canExecuteAction)
        {
            return new Rule<T>(Context, () =>
            {
                var result = PreviousRule.CanExecuteAction();
                if (result.BypassRules) return (result.Exception, true);
                if (result.Exception != null) return (result.Exception, true);
                return (canExecuteAction(), false);
            });
        }
    }
}
