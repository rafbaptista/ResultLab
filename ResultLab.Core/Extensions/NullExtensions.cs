using Result.Core.Models;
using System;

namespace Result.Core.Extensions
{
    public static class NullExtensions
    {
        /// <summary>
        /// Não retorna nenhum resultado, logo, toda a execução de código continua
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result<T> OnNull<T>(this Result<T> result, Action action)
        {
            if (result.IsFailure || !result.IsNull) return result;

            action.Invoke();
            return result;
        }

        /// <summary>
        /// Retorna resultado e é possível parar a execução do restante do código
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result<T> OnNullReturn<T>(this Result<T> result, Func<Result<T>> action)
        {
            if (result.IsFailure || !result.IsNull) return result;

            return action.Invoke();
        }

        public static TReturn OnNullReturn<TResult, TReturn>(this Result<TResult> result, Func<TReturn> action)
        {
            if (result.IsNull)
                return action.Invoke();
            else
                return default(TReturn);
        }
    }
}
