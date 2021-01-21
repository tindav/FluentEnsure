using System;

namespace FluentEnsure.Sdk
{
    internal abstract class RuleCombinationBase<T> : ContextContainerBase<T>, IRealBeforeRule<T>
    {
        public IRealRule<T> PreviousRule { get; }

        protected RuleCombinationBase(IRealRule<T> previousRule)
            : base(previousRule.Context)
        {
            PreviousRule = previousRule;
        }

        public abstract IRealRule<T> CombineWith(Func<Exception> canExecuteAction);
    }
}
