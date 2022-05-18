//using Moq;
//using ResultLab.Core.Extensions;
//using ResultLab.WebApi.Domain;
//using ResultLab.WebApi.Interfaces;
//using System;
//using Xunit;

//namespace ResultLab.Tests.Extensions
//{
//    public class FailureExtensionsTests
//    {
//        private readonly Mock<IPersonRepository> _mockPersonRepository;
//        private readonly Mock<ICarRepository> _mockCarRepository;

//        public FailureExtensionsTests()
//        {
//            _mockPersonRepository = new Mock<IPersonRepository>();
//            _mockCarRepository = new Mock<ICarRepository>();
//        }


//        [Fact(DisplayName = "OnFailureReturn: Deve encerrar o fluxo do método")]
//        [Trait("Categoria", "Failure Extensions")]
//        public void OnFailureReturn_DeveEncerrrarOFluxoDoMetodo()
//        {
//            //Arrange
//            _mockPersonRepository
//                .Setup(mock => mock.GetByName(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Failure<Person>("erro"));

//            _mockCarRepository
//                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

//            //Act
//            var person = _mockPersonRepository.Object.GetByName("Rafael");

//            var result = person.OnFailureReturn<Person>(ex =>
//            {
//                return Core.Models.Result.Failure<Person>(new TimeoutException("Tempo limite esgotado"));
//                _mockCarRepository.Object.GetByModel("Corsa");
//            });

//            //Assert
//            Assert.True(result.Exception is not null);
//            Assert.True(result.IsFailure);
//            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
//        }


//        [Fact(DisplayName = "OnFailureReturn: Deve encerrar o fluxo do método")]
//        [Trait("Categoria", "Failure Extensions")]
//        public void OnSuccessReturn_DeveEncerrrarOFluxoDoMetodo_ComOTipoInformado_EmRespostasPositivas()
//        {
//            //Arrange
//            _mockPersonRepository
//                .Setup(mock => mock.GetByName(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Success<Person>(new Person { Name = "Rafael" }));

//            _mockCarRepository
//                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

//            //Act
//            var person = _mockPersonRepository.Object.GetByName("Rafael");

//            var result = person.OnFailureReturn<Person, bool>(p =>
//            {
//                return true; //não é executado, pois o resultado foi Success
//                _mockCarRepository.Object.GetByModel("Corsa");
//            });

//            //Assert
//            Assert.True(result == default(bool));
//            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
//        }

//        [Fact(DisplayName = "OnFailureReturn: Deve encerrar o fluxo do método")]
//        [Trait("Categoria", "Failure Extensions")]
//        public void OnSuccessReturn_DeveEncerrrarOFluxoDoMetodo_ComOTipoInformado_EmRespostasNegativas()
//        {
//            //Arrange
//            _mockPersonRepository
//                .Setup(mock => mock.GetByName(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Failure<Person>("erro"));

//            _mockCarRepository
//                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

//            //Act
//            var person = _mockPersonRepository.Object.GetByName("Rafael");

//            var result = person.OnFailureReturn<Person, bool>(p =>
//            {
//                return true;
//                _mockCarRepository.Object.GetByModel("Corsa");
//            });

//            //Assert
//            Assert.True(result);
//            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
//        }


//        [Fact(DisplayName = "OnFailure: Não deve encerrar o fluxo do método")]
//        [Trait("Categoria", "Failure Extensions")]
//        public void OnFailure_NaoDeveEncerrrarOFluxoDoMetodo()
//        {
//            //Arrange
//            _mockPersonRepository
//                .Setup(mock => mock.GetByName(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Failure<Person>("erro"));

//            _mockCarRepository
//                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

//            //Act
//            var person = _mockPersonRepository.Object.GetByName("Rafael");

//            var result = person.OnFailure(ex =>
//            {
//                _mockCarRepository.Object.GetByModel("Corsa");
//            });

//            //Assert
//            Assert.NotNull(result);
//            Assert.True(result.IsFailure);
//            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Once);
//        }

//        [Fact(DisplayName = "OnFailure: Não deve executar o fluxo do método, pois objeto não está com estado de falha")]
//        [Trait("Categoria", "Failure Extensions")]
//        public void OnFailure_NaoDeveEncerrrarOFluxoDoMetodo_PoisObjetoNaoEstaFailure()
//        {
//            //Arrange
//            _mockPersonRepository
//                .Setup(mock => mock.GetByName(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Null<Person>());

//            _mockCarRepository
//                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

//            //Act
//            var person = _mockPersonRepository.Object.GetByName("Rafael");

//            var result = person.OnFailure(ex =>
//            {
//                _mockCarRepository.Object.GetByModel("Corsa");
//            });

//            //Assert
//            Assert.True(result.IsNull);
//            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
//        }

//    }
//}
