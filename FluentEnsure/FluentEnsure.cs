using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using FluentEnsure.Sdk;

namespace FluentEnsure
{
    public static class FluentEnsure
    {
        [Pure]
        public static IRule<T> That<T>(this IBeforeRule<T> beforeRule, Func<bool> lazyCondition, Exception exception = null)
        {
            var realBefore = (IRealBeforeRule<T>)beforeRule;
            return realBefore.CombineWith(() => lazyCondition() ? null : exception ?? Exceptions.GetDefaultException());
        }

        [Pure]
        public static IRule<T> That<T>(this IBeforeRule<T> beforeRule, Func<T, bool> lazyCondition, Exception exception = null)
        {
            var realBefore = (IRealBeforeRule<T>)beforeRule;
            return realBefore.CombineWith(() => lazyCondition(realBefore.Context) ? null : exception ?? Exceptions.GetDefaultException());
        }

        [Pure]
        public static IRule<T> That<T>(this IBeforeRule<T> beforeRule, bool condition, Exception exception = null)
        {
            var realBefore = (IRealBeforeRule<T>)beforeRule;
            return realBefore.CombineWith(() => condition ? null : exception ?? Exceptions.GetDefaultException());
        }

        [Pure]
        public static IBeforeRule<T> Or<T>(this IRule<T> rule)
        {
            var realRule = (IRealRule<T>)rule;
            return new OrOperator<T>(realRule);
        }

        [Pure]
        public static IBeforeRule<T> And<T>(this IRule<T> rule)
        {
            var realRule = (IRealRule<T>)rule;
            return new AndOperator<T>(realRule);
        }

        public static void OrThrow<T>(this IRule<T> rule, [CallerMemberName] string callingMethod = null)
        {
            var realRule = (IRealRule<T>)rule;
            var result = realRule.CanExecuteAction();
            if (result.Exception == null) return;
            throw result.Exception;
        }

        public static bool GetResult<T>(this IRule<T> rule)
        {
            var realRule = (IRealRule<T>)rule;
            return realRule.CanExecuteAction().Exception == null;
        }

        [Pure]
        public static IBeforeRule<object> StartEnsure()
        {
            return new EnsureStart();
        }

        [Pure]
        public static IBeforeRule<TNew> WithContext<TOld, TNew>(this IBeforeRule<TOld> beforeRule, TNew context)
        {
            var realBeforeRule = (IRealBeforeRule<TOld>)beforeRule;
            return new ContextSwitcher<TOld, TNew>(realBeforeRule, context);
        }
    }
}
