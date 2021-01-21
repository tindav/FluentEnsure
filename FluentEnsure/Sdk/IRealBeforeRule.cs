using System;
using FluentEnsure;

namespace FluentEnsure.Sdk
{
    public interface IRealBeforeRule<out T> : IBeforeRule<T>, IContextContainer<T>
    {
        IRealRule<T> CombineWith(Func<Exception> canExecuteAction);
    }
}
