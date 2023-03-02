using inventario.business.Abstractions.Data;
using inventario.business.Models;
using inventario.business.Models.Request;
using inventario.business.Models.Response;
using inventario.business.Service;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace inventario.business.test.Service
{
    public class CategoriaServiceTest
    {
        private readonly MockRepository _mocks = new(MockBehavior.Strict);

        Mock<ICategoriaRepository> _categoriaRepository;

        [SetUp]
        public void SetUp()
        {
            _categoriaRepository = _mocks.Create<ICategoriaRepository>(MockBehavior.Strict);
        }

        [Test]
        public async Task CategoriaService_AdicionarAsync_Sucesso()
        {
            // Arrange
            CategoriaRequest request = new() { Ativo = true, Nome = "Copo" };

            _categoriaRepository
                .Setup(x => x.AdicionarAsync(It.Is<CategoriaModel>(t => t.Id.Equals(request.Id)), CancellationToken.None))
                .Returns(Task.CompletedTask);

            CategoriaService service = GetService();

            // Act
            CategoriaResponse result = await service.AdicionarAsync(request);

            // Assert
            Assert.IsNotNull(result);
            _mocks.Verify();
        }

        [Test]
        public async Task CategoriaService_AtualizarAsync_Sucesso()
        {
            // Arrange

            CategoriaRequest request = new() { Ativo = false, Nome = "Xícara" };

            _categoriaRepository.Setup(x => x.AlterarAsync(It.Is<CategoriaModel>(t => t.Id.Equals(request.Id)), CancellationToken.None))
                .Returns(Task.CompletedTask);

            CategoriaService service = GetService();

            // Act
            CategoriaResponse result = await service.AlterarAsync(request);

            // Assert
            Assert.IsNotNull(result);
            _mocks.Verify();
        }

        private CategoriaService GetService() => new(
            _categoriaRepository.Object
        );
    }
}
