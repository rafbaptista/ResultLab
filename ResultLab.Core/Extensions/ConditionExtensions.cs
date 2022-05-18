using Result.Core.Models;
using System;

namespace Result.Core.Extensions
{
    public static class ConditionExtensions
    {
        public static Result<T> OnCondition<T>(this Result<T> result, Func<T, bool> predicate, Action<T> action)
        {
            bool predicateResult = predicate.Invoke(result.Data);

            if (!predicateResult)
                return result;

            action.Invoke(result.Data);
            return result;
        }

        public static Result<T> OnConditionReturn<T>(this Result<T> result, Func<T, bool> predicate, Func<T, Result<T>> action)
        {
            bool predicateResult = predicate.Invoke(result.Data);

            if (!predicateResult)
                return result;

            return action.Invoke(result.Data);
        }

        public static TReturn OnConditionReturn<TResult, TReturn>(this Result<TResult> result, Func<TResult, bool> predicate, Func<Result<TResult>, TReturn> action)
        {
            bool predicateResult = predicate.Invoke(result.Data);

            if (predicateResult)
                return action.Invoke(result);
            else
                return default(TReturn);
        }

    }
}
