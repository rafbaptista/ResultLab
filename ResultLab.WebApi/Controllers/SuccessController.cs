using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResultLab.Api.Domain;
using ResultLab.Api.Interfaces;
using ResultLab.Core.Extensions;

namespace ResultLab.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SuccessController : ControllerBase
    {
        private readonly ILogger<SuccessController> _logger;
        private readonly IComputerRepository _computerRepository;

        public SuccessController(
            ILogger<SuccessController> logger, 
            IComputerRepository computerRepository)
        {
            _logger = logger;
            _computerRepository = computerRepository;
        }

        [HttpGet]
        public string GetComputerByOwner(string owner)
        {
            var computer = _computerRepository.GetByOwner(owner);

            //só realiza a ação se Result<computer> for sucesso
            computer.OnSuccess(c => _logger.LogInformation($"Sucesso ao obter o computador de Dono {c.Owner}"));

            //continua fluxo atual da função
            return "Ok";
        }

        [HttpGet]
        public string GetComputerByOwnerAndReturn(string owner = "Rafael")
        {
            var computer = _computerRepository.GetByOwner(owner);

            //retorna mensagem de sucesso em caso de sucesso
            //retorna mensagem de falha em caso de falha
            //encerra fluxo da função atual

            return computer.OnSuccess(success: c =>
            {
                return $"Sucesso ao obter computador Id {c.Id} do dono {c.Owner}";
            },
            failure: error => 
            {
                return $"Falha ao obter computador de Dono {owner}, {error}";
            });

            //ou..
            //return computer
            //        .OnSuccess(success: c => LogSuccessInformation(c), 
            //                   failure: error => LogFailureInformation(error,owner));

        }

        private string LogSuccessInformation(Computer c) => $"Sucesso ao obter computador Id {c.Id} do dono {c.Owner}";
        private string LogFailureInformation(string error, string owner) => $"Falha ao obter computador de Dono {owner}, {error}";

    }
}
