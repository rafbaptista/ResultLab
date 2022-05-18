using ResultLab.Core.Models;
using System;

namespace ResultLab.Core.Extensions
{
    public static class ConditionExtensions
    {
        /// <summary>
        /// Executa uma ação se a condição contida em <paramref name="predicate"/> for satisfeita
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">Resultado de uma operação, que pode ser sucesso ou falha</param>
        /// <param name="predicate">A condição que deverá ser satisfeita</param>
        /// <param name="action">A ação a ser executada caso a condição seja satisfeita</param>
        /// <returns></returns>
        public static Result<T> OnCondition<T>(this Result<T> result, Func<T, bool> predicate, Action<T> action)
        {
            bool predicateResult = predicate.Invoke(result.Data);

            if (predicateResult)
                action.Invoke(result.Data);

            return result;
        }

        /// <summary>
        /// Executa uma ação se a condição for satisfeita e outra se a condição não for satisfeta. Também pode ser utilizada junto com um return para encerrar a função pai e o escopo atual
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="result">Resultado de uma operação, que pode ser sucesso ou falha</param>
        /// <param name="predicate">A condição que deverá ser satisfeita</param>
        /// <param name="success">Ação que será realizada e retornada caso a condição seja satisfeita</param>
        /// <param name="failure">Ação que será realizada e retornada caso a condição não seja satisfeita</param>
        /// <returns></returns>
        public static TReturn OnCondition<T, TReturn>(this Result<T> result, Func<T, bool> predicate, Func<T, TReturn> success, Func<T, TReturn> failure)
        {
            bool predicateResult = predicate.Invoke(result.Data);

            if (!predicateResult)
                return failure.Invoke(result.Data);

            return success.Invoke(result.Data);
        }
    }
}
