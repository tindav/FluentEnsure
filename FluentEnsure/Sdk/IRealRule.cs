using System;

namespace FluentEnsure.Sdk
{
    public interface IRealRule<out T> : IRule<T>, IContextContainer<T>
    {
        Func<(Exception Exception, bool BypassRules)> CanExecuteAction { get; }
    }
}
