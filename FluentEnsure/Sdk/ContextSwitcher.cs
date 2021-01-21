using System;

namespace FluentEnsure.Sdk
{
    internal class ContextSwitcher<TOld, TNew> : ContextContainerBase<TNew>, IRealBeforeRule<TNew>
    {
        private IRealBeforeRule<TOld> PreviousBeforeRule { get; }

        public ContextSwitcher(IRealBeforeRule<TOld> previousBeforeRule, TNew context) : base(context)
        {
            PreviousBeforeRule = previousBeforeRule;
        }

        public IRealRule<TNew> CombineWith(Func<Exception> canExecuteAction)
        {
            return new Rule<TNew>(Context, PreviousBeforeRule.CombineWith(canExecuteAction).CanExecuteAction);
        }
    }
}
