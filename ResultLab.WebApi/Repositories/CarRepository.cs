using ResultLab.Core.Models;
using ResultLab.WebApi.Domain;
using ResultLab.WebApi.Interfaces;
using System;

namespace ResultLab.WebApi.Repositories
{
    public class CarRepository : ICarRepository
    {
        public Result<Car> GetByModel(string model)
        {
            return Result.Success<Car>(new Car { Model = model });   
        }
    }
}
