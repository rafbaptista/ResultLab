using Result.Core.Models;
using Result.WebApi.Domain;
using Result.WebApi.Interfaces;
using System;

namespace Result.WebApi.Repositories
{
    public class CarRepository : ICarRepository
    {
        public Result<Car> GetByModel(string model)
        {
            throw new NotImplementedException();
        }
    }
}
