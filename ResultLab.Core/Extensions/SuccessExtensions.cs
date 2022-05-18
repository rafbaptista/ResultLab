using ResultLab.Core.Models;
using System;

namespace ResultLab.Core.Extensions
{
    public static class SuccessExtensions 
    {
        /// <summary>
        /// Executa uma ação caso <paramref name="result"/> seja sucesso
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">Resultado de uma operação, que pode ser sucesso ou falha</param>
        /// <param name="action">A ação a ser executada em caso de sucesso</param>
        /// <returns></returns>
        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
                action.Invoke(result.Data);

            return result;
        }

        /// <summary>
        /// Executa uma ação caso <paramref name="result"/> seja sucesso, outra ação caso <paramref name="result"/> seja falha. Também pode ser utilizada junto com um return para encerrar a função pai e o escopo atual
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="result">Resultado de uma operação, que pode ser sucesso ou falha</param>
        /// <param name="success">Ação que será realizada e retornada caso <paramref name="result"/> seja sucesso</param>
        /// <param name="failure">Ação que será realizada e retornada caso <paramref name="result"/> seja falha</param>
        /// <returns></returns>
        public static TReturn OnSuccess<T, TReturn>(this Result<T> result, Func<T, TReturn> success, Func<string,TReturn> failure)
        {
            if (result.IsSuccess)
                return success.Invoke(result.Data);
            else
                return failure.Invoke(result.Message);
        }
    }
}
