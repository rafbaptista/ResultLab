using Moq;
using Result.Core.Extensions;
using Result.WebApi.Domain;
using Result.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Result.Tests.Extensions
{
    public class NullExtensionsTests
    {
        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly Mock<ICarRepository> _mockCarRepository;
        public NullExtensionsTests()
        {
            _mockPersonRepository = new Mock<IPersonRepository>();
            _mockCarRepository = new Mock<ICarRepository>();
        }

        [Fact(DisplayName = "OnNullReturn: Deve encerrar o fluxo do método")]
        [Trait("Categoria", "Null Extensions")]
        public void OnNullReturn_DeveEncerrrarOFluxoDoMetodo()
        {
            //Arrange
            _mockPersonRepository
                .Setup(mock => mock.GetByName(It.IsAny<string>()))
                .Returns(Core.Models.Result.Null<Person>);

            _mockCarRepository
                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

            //Act
            var person = _mockPersonRepository.Object.GetByName("Rafael");

            var result = person.OnNullReturn(() =>
            {
                return Core.Models.Result.Success<Person>(new Person { Name = "Rafael" }); //retorna success apesar do nulo recebido, pois necessita Result<T>
                _mockCarRepository.Object.GetByModel("Corsa");
            });

            //Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsNull);
            Assert.True(result.Data.Name == "Rafael");
            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
        }


        [Fact(DisplayName = "OnNull: Não deve encerrar o fluxo do método")]
        [Trait("Categoria", "Null Extensions")]
        public void OnNull_NaoDeveEncerrrarOFluxoDoMetodo()
        {
            //Arrange
            _mockPersonRepository
                .Setup(mock => mock.GetByName(It.IsAny<string>()))
                .Returns(Core.Models.Result.Null<Person>);

            _mockCarRepository
                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

            //Act
            var person = _mockPersonRepository.Object.GetByName("Rafael");

            var result = person.OnNull(() =>
            {
                _mockCarRepository.Object.GetByModel("Corsa");
            });

            //Assert
            Assert.Null(result.Data);
            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Once);
        }

        [Fact(DisplayName = "OnNull: Ddeve encerrar o fluxo do método com o tipo informado")]
        [Trait("Categoria", "Null Extensions")]
        public void OnNullReturn_DeveEncerrrarOFluxoDoMetodo_ComOTipoInformado()
        {
            //Arrange
            _mockPersonRepository
                .Setup(mock => mock.GetByName(It.IsAny<string>()))
                .Returns(Core.Models.Result.Null<Person>);

            _mockCarRepository
                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

            //Act
            var person = _mockPersonRepository.Object.GetByName("Rafael");

            var result = person.OnNullReturn<Person, int>(() =>
             {
                 return 450;
                _mockCarRepository.Object.GetByModel("Corsa");
             });

            //Assert
            Assert.Equal(450, result);
            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
        }

        [Fact(DisplayName = "OnNull: Ddeve encerrar o fluxo do método com o tipo informado")]
        [Trait("Categoria", "Null Extensions")]
        public void OnNullReturn_NaoDeveEncerrrarOFluxoDoMetodo_PoisObjetoNaoEstaNulo()
        {
            //Arrange
            _mockPersonRepository
                .Setup(mock => mock.GetByName(It.IsAny<string>()))
                .Returns(Core.Models.Result.Success<Person>(new Person { Name = "Rafael" }));

            _mockCarRepository
                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

            //Act
            var person = _mockPersonRepository.Object.GetByName("Rafael");

            var result = person.OnNullReturn<Person, int>(() =>
            {
                //nada deve ser executado, pois obj não é nulo
                return 450;
                _mockCarRepository.Object.GetByModel("Corsa");
            });

            //Assert
            Assert.Equal(default(int), result);
            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
        }
    }
}
