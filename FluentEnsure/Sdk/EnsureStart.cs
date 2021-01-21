using System;

namespace FluentEnsure.Sdk
{
    internal class EnsureStart : ContextContainerBase<object>, IRealBeforeRule<object>
    {
        public EnsureStart() : base(new object())
        {
        }

        public IRealRule<object> CombineWith(Func<Exception> canExecuteAction)
        {
            return new Rule<object>(Context, () => (canExecuteAction(), false));
        }
    }
}
