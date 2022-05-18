using ResultLab.Core.Models;
using ResultLab.WebApi.Domain;
using ResultLab.WebApi.Interfaces;
using System;

namespace ResultLab.WebApi.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public Result<Person> GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
