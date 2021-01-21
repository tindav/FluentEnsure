using System;

namespace FluentEnsure.Sdk
{
    internal class OrOperator<T> : RuleCombinationBase<T>, IRealBeforeRule<T>
    {
        public OrOperator(IRealRule<T> previousRule) : base(previousRule)
        {
        }

        public override IRealRule<T> CombineWith(Func<Exception> canExecuteAction)
        {
            return new Rule<T>(Context, () =>
            {
                var result = PreviousRule.CanExecuteAction();
                if (result.BypassRules) return result;
                if (result.Exception == null) return (result.Exception, true);
                return (canExecuteAction(), false);
            });
        }
    }
}
