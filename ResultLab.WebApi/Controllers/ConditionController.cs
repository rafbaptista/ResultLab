using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResultLab.Api.Domain;
using ResultLab.Api.Interfaces;
using ResultLab.Core.Extensions;
using ResultLab.Core.Models;

namespace ResultLab.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ConditionController : ControllerBase
    {
        private readonly ILogger<ConditionController> _logger;
        private readonly IComputerRepository _computerRepository;

        public ConditionController(
            ILogger<ConditionController> logger,
            IComputerRepository computerRepository)
        {
            _logger = logger;
            _computerRepository = computerRepository;
        }

        [HttpGet]
        public string GetComputerByMemory(int memory)
        {
            var computer = _computerRepository.GetByMemory(memory);

            //só loga a mensagem se memory < 6 e 
            computer
                .OnCondition(c => c.Memory < 6, c =>
                {
                    _logger.LogInformation($"O computador \"{c.Name}\" possui menos de 6GB de memória ram");
                });

            //ou...
            //computer.OnCondition(predicate: c => WithMemoryLessThan(c.Memory), action: c => LogSomeInformation(c));

            //continua o fluxo da função atual
            return "Ok";
        }

        private void LogSomeInformation(Computer computer) => _logger.LogInformation($"O computador \"{computer.Name}\" possui menos de 6GB de memória ram");
        private bool WithMemoryLessThan(int memory) => memory < 6;


        /// <summary>
        /// O método OnCondition utilizado junto com o return retorna um valor e para de executar a função pai (GetComputerByMemoryAndReturn)
        /// </summary>
        /// <param name="memory"></param>
        /// <returns></returns>
        [HttpGet]
        public Result<string> GetComputerByMemoryAndReturn(int memory)
        {
            var computer = _computerRepository.GetByMemory(memory);

            //retorna sucesso caso memory == 6
            //retorna falha caso memory != 6
            //encerra o fluxo da função

            return computer.OnCondition(
                predicate: c => c?.Memory == 6,
                success: c =>
                {
                    return Result.Success<string>($"O computador {c.Name} foi encontrado e contém {c.Memory} GB de memória RAM");
                },
                failure: c =>
                {
                    return Result.Failure<string>($"Nenhum computador foi encontrado contendo {memory} GB de memória RAM");
                });

            //ou...

            //return computer
            //    .OnCondition
            //    (
            //        predicate: c => WithMemoryEquals(c.Memory), 
            //        returnIfTrue: c => HandleTrueCondition(c), 
            //        returnIfFalse: c => HandleFalseCondition(c)
            //    );

        }

        private bool WithMemoryEquals(int memory) => memory == 6;
        private Result<string> HandleTrueCondition(Computer c) => Result.Success<string>($"O computador {c.Name} foi encontrado e contém {c.Memory} GB de memória RAM");
        private Result<string> HandleFalseCondition(Computer c) => Result.Failure<string>($"Nenhum computador foi encontrado contendo {c.Memory} GB de memória RAM");

    }
}
