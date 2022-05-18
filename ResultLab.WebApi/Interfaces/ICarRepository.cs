using ResultLab.WebApi.Domain;

namespace ResultLab.WebApi.Interfaces
{
    public interface ICarRepository
    {
        Core.Models.Result<Car> GetByModel(string model);
    }
}
