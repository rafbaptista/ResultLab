using ResultLab.Core.Models;
using System;

namespace ResultLab.Core.Extensions
{
    public static class FailureExtensions
    {
        /// <summary>
        /// Executa uma ação caso o resultado de <paramref name="result"/> seja um erro/falha
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">Resultado de uma operação, que pode ser sucesso ou falha</param>
        /// <param name="action">A ação a ser executada em caso de falha</param>
        /// <returns></returns>
        public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
        {
            if (result.IsFailure)
                action.Invoke(result.Message);

            return result;
        }

        /// <summary>
        /// Executa uma ação caso <paramref name="result"/> seja falha, outra ação caso <paramref name="result"/> seja sucesso. Também pode ser utilizada junto com um return para encerrar a função pai e o escopo atual
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="result">Resultado de uma operação, que pode ser sucesso ou falha</param>
        /// <param name="failure">Ação que será realizada e retornada caso <paramref name="result"/> seja falha</param>
        /// <param name="success">Ação que será realizada e retornada caso <paramref name="result"/> seja sucesso</param>
        /// <returns></returns>
        public static TReturn OnFailure<T, TReturn>(this Result<T> result, Func<string, TReturn> failure, Func<T, TReturn> success)
        {
            if (result.IsFailure)
                return failure.Invoke(result.Message);
            else
                return success.Invoke(result.Data);
        }

    }
}
