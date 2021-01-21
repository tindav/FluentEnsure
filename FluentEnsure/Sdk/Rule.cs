using System;

namespace FluentEnsure.Sdk
{
    internal class Rule<T> : ContextContainerBase<T>, IRealRule<T>
    {
        public Func<(Exception Exception, bool BypassRules)> CanExecuteAction { get; }

        public Rule(T context, Func<(Exception Exception, bool BypassRules)> canExecuteAction) : base(context)
        {
            CanExecuteAction = canExecuteAction;
        }
    }
}
