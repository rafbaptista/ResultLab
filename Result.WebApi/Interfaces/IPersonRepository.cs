using Result.WebApi.Domain;

namespace Result.WebApi.Interfaces
{
    public interface IPersonRepository
    {
        Core.Models.Result<Person> GetByName(string name);
    }
}
