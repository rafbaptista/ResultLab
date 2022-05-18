using ResultLab.WebApi.Domain;

namespace ResultLab.WebApi.Interfaces
{
    public interface IPersonRepository
    {
        Core.Models.Result<Person> GetByName(string name);
    }
}
