using Moq;
using ResultLab.Core.Extensions;
using ResultLab.WebApi.Domain;
using ResultLab.WebApi.Interfaces;
using Xunit;

namespace ResultLab.Tests.Extensions
{
    public class ConditionExtensionsTests
    {
        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly Mock<ICarRepository> _mockCarRepository;

        public ConditionExtensionsTests()
        {
            _mockPersonRepository = new Mock<IPersonRepository>();
            _mockCarRepository = new Mock<ICarRepository>();
        }

    }
}
