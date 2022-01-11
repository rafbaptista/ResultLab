using Result.Core.Models;
using System;

namespace Result.Core.Extensions
{
    public static class FailureExtensions
    {
        /// <summary>
        /// Executa uma função que não retorna valor e não encerra a execução do fluxo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result<T> OnFailure<T>(this Result<T> result, Action<Exception> action)
        {
            if (!result.IsFailure) return result;

            action.Invoke(result.Exception);
            return result;
        }

        /// <summary>
        ///  Executa uma função que deve retornar um valor de tipo <typeparamref name="T"/> e encerra a execução do fluxo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result<T> OnFailureReturn<T>(this Result<T> result, Func<Exception, Result<T>> action)
        {
            if (!result.IsFailure) return result;

            return action.Invoke(result.Exception);
        }

        /// Executa uma função que deve retornar um valor de tipo especificado e encerra a execução do fluxo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TReturn OnFailureReturn<TResult, TReturn>(this Result<TResult> result, Func<Result<TResult>, TReturn> action)
        {
            if (result.IsFailure)
                return action.Invoke(result);
            else
                return default(TReturn);
        }

    }
}
