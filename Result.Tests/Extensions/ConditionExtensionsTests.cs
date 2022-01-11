using Moq;
using Result.Core.Extensions;
using Result.WebApi.Domain;
using Result.WebApi.Interfaces;
using Xunit;

namespace Result.Tests.Extensions
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

        [Fact(DisplayName = "OnConditionReturn: Deve encerrar o fluxo do método")]
        [Trait("Categoria", "Condition Extensions")]
        public void OnFailureReturn_DeveEncerrrarOFluxoDoMetodo()
        {
            //Arrange
            _mockPersonRepository
                .Setup(mock => mock.GetByName(It.IsAny<string>()))
                .Returns(Core.Models.Result.Success<Person>(new Person { Name = "XPTO" }));

            _mockCarRepository
                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

            //Act
            var person = _mockPersonRepository.Object.GetByName("Rafael");

            var result = person.OnConditionReturn<Person>(p => p.Name == "XPTO", p =>
            {
                return Core.Models.Result.Failure<Person>("erro");
                _mockCarRepository.Object.GetByModel("Corsa");
            });

            //Assert
            Assert.True(result.IsFailure);
            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
        }

        [Fact(DisplayName = "OnConditionReturn: Não deve satisfazer a condição")]
        [Trait("Categoria", "Condition Extensions")]
        public void OnFailureReturn_NaoDeveSatisfazerACondicao()
        {
            //Arrange
            _mockPersonRepository
                .Setup(mock => mock.GetByName(It.IsAny<string>()))
                .Returns(Core.Models.Result.Success<Person>(new Person { Name = "XPTO" }));

            _mockCarRepository
                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

            //Act
            var person = _mockPersonRepository.Object.GetByName("Rafael");

            var result = person.OnConditionReturn<Person>(p => p.Name == "Rafael", p =>
            {
                return Core.Models.Result.Failure<Person>("erro");
                _mockCarRepository.Object.GetByModel("Corsa");
            });

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Data.Name == "XPTO");
            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
        }

        [Fact(DisplayName = "OnConditionReturn: Deve encerrar o fluxo do método com o tipo fornecido")]
        [Trait("Categoria", "Condition Extensions")]
        public void OnFailureReturn_DeveEncerrrarOFluxoDoMetodo_ComOTipoFornecido()
        {
            //Arrange
            _mockPersonRepository
                .Setup(mock => mock.GetByName(It.IsAny<string>()))
                .Returns(Core.Models.Result.Success<Person>(new Person { Name = "XPTO" }));

            _mockCarRepository
                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

            //Act
            var person = _mockPersonRepository.Object.GetByName("Rafael");

            var result = person.OnConditionReturn<Person,int>(p => p.Name == "XPTO", p =>
            {
                return 500;
                _mockCarRepository.Object.GetByModel("Corsa");
            });

            //Assert
            Assert.Equal(500, result);
            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
        }

    }
}
