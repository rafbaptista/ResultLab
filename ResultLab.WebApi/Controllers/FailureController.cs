using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResultLab.Api.Domain;
using ResultLab.Api.Interfaces;
using ResultLab.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultLab.Api.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class FailureController : ControllerBase
    {
        private readonly ILogger<FailureController> _logger;
        private readonly IComputerRepository _computerRepository;

        public FailureController(
            ILogger<FailureController> logger, 
            IComputerRepository computerRepository)
        {
            _logger = logger;
            _computerRepository = computerRepository;
        }

        [HttpGet]
        public string GetComputerByOwner(string owner = "xpto")
        {
            var computer = _computerRepository.GetByOwner(owner);

            //só realiza uma ação se a requisição for sucedida
            computer.OnFailure(error => _logger.LogInformation($"Falha ao obter computer da base {error}"));

            //continua fluxo da função atual
            return "Falha";
        }

        [HttpGet]
        public string GetComputerByOwnerAndReturn(string owner = "xpto")
        {
            var computer = _computerRepository.GetByOwner(owner);

            //retorna mensagem de falha em caso de falha
            //retorna mensagem de sucesso em caso de sucesso
            //encerra fluxo da função atual

            return computer.OnFailure(failure: error =>
            {
                return $"Falha ao obter computador. {error}";
            },
            success: c =>
            {
                return $"Sucesso ao obter computador da base. Id: {c.Id}, Nome: {c.Name}";
            });

            //ou..
            //return computer
            //        .OnFailure(failure: error => LogFailureInformation(error, owner), 
            //                   success: c => LogSuccessInformation(c));

        }

        private string LogSuccessInformation(Computer c) => $"Sucesso ao obter computador Id {c.Id} do dono {c.Owner}";
        private string LogFailureInformation(string error, string owner) => $"Falha ao obter computador de Dono {owner}, {error}";


    }
}
