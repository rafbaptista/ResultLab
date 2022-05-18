using Result.Core.Models;
using System;

namespace Result.Core.Extensions
{
    public static class SuccessExtensions 
    {
        /// <summary>
        /// Executa uma função que não retorna valor e não encerra a execução do fluxo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (!result.IsSuccess) return result;

            action.Invoke(result.Data);
            return result;
        }

        /// <summary>
        /// Executa uma função que deve retornar um valor de tipo <typeparamref name="T"/> e encerra a execução do fluxo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Result<T> OnSuccessReturn<T>(this Result<T> result, Func<T, Result<T>> action)
        {
            if (!result.IsSuccess) return result;

            return action.Invoke(result.Data);
        }

        /// Executa uma função que deve retornar um valor de tipo especificado e encerra a execução do fluxo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TReturn OnSuccessReturn<TResult,TReturn>(this Result<TResult> result, Func<Result<TResult>, TReturn> action) 
        {
            if (result.IsSuccess)
                return action.Invoke(result);
            else
                return default(TReturn);
        }


    }
}
