//using Moq;
//using ResultLab.Core.Extensions;
//using ResultLab.WebApi.Domain;
//using ResultLab.WebApi.Interfaces;
//using Xunit;

//namespace ResultLab.Tests.Extensions
//{
//    public class SuccessExtensionsTests
//    {
//        private readonly Mock<IPersonRepository> _mockPersonRepository;
//        private readonly Mock<ICarRepository> _mockCarRepository;
//        public SuccessExtensionsTests()
//        {
//            _mockPersonRepository = new Mock<IPersonRepository>();
//            _mockCarRepository = new Mock<ICarRepository>();
//        }

//        [Fact(DisplayName = "OnSuccessReturn: Deve encerrar o fluxo do método")]
//        [Trait("Categoria", "Success Extensions")]
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

//            var result = person.OnSuccessReturn<Person,bool>(p =>
//            {
//                return true;
//                _mockCarRepository.Object.GetByModel("Corsa");
//            });

//            //Assert
//            Assert.True(result);
//            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
//        }

//        [Fact(DisplayName = "OnSuccessReturn: Deve encerrar o fluxo do método")]
//        [Trait("Categoria", "Success Extensions")]
//        public void OnSuccessReturn_DeveEncerrrarOFluxoDoMetodo_ComOTipoInformado_EmRespostasNegativas()
//        {
//            //Arrange
//            _mockPersonRepository
//                .Setup(mock => mock.GetByName(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Failure<Person>(new Person()));

//            _mockCarRepository
//                .Setup(mock => mock.GetByModel(It.IsAny<string>()))
//                .Returns(Core.Models.Result.Success<Car>(new Car { Model = "Corsa" }));

//            //Act
//            var person = _mockPersonRepository.Object.GetByName("Rafael");

//            var result = person.OnSuccessReturn<Person, bool>(p =>
//            {
//                return true; //não é executado
//                _mockCarRepository.Object.GetByModel("Corsa");
//            });

//            //Assert
//            Assert.True(result == default(bool));
//            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Never);
//        }

//        [Fact(DisplayName = "OnSuccess: Não deve encerrar o fluxo do método")]
//        [Trait("Categoria", "Success Extensions")]
//        public void OnSuccess_NaoDeveEncerrrarOFluxoDoMetodo()
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

//            var result = person.OnSuccess(ex =>
//            {
//                _mockCarRepository.Object.GetByModel("Corsa");
//            });

//            //Assert
//            Assert.NotNull(result);
//            Assert.True(result.IsSuccess);
//            _mockCarRepository.Verify(x => x.GetByModel("Corsa"), Times.Once);
//        }

//    }
//}
