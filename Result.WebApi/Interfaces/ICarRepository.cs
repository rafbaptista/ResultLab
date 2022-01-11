using Result.WebApi.Domain;

namespace Result.WebApi.Interfaces
{
    public interface ICarRepository
    {
        Core.Models.Result<Car> GetByModel(string model);
    }
}
