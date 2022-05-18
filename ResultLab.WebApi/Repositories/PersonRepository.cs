using Result.Core.Models;
using Result.WebApi.Domain;
using Result.WebApi.Interfaces;
using System;

namespace Result.WebApi.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public Result<Person> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
