using ResultLab.Api.Domain;
using ResultLab.Core.Models;

namespace ResultLab.Api.Interfaces
{
    public interface IComputerRepository
    {
        Result<Computer> GetByMemory(int memory);
        Result<Computer> GetByOwner(string owner); 
    }
}
