using ResultLab.Api.Domain;
using ResultLab.Api.Interfaces;
using ResultLab.Core.Models;

namespace ResultLab.Api.Repositories
{
    public class ComputerRepository : IComputerRepository
    {
        public Result<Computer> GetByMemory(int memory)
        {
            return Result.Success<Computer>(new Computer { Name = "Computador do Rafa", Memory = memory });
        }

        public Result<Computer> GetByOwner(string owner)
        {
            if (owner == "Rafael")
                return Result.Success<Computer>(new Computer { Id = 1, Memory = 6, Name = "Computador do Rafa", Owner = owner });
            else
                return new Result<Computer> { Data = null, IsFailure = true, Message = "Erro ao obter computador na base de dados" };
        }
    }
}
