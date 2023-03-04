using inventario.business.Abstractions.Data;
using inventario.business.Models;
using inventario.business.Models.Request;
using inventario.business.Models.Response;
using inventario.business.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace inventario.business.test.Service
{
    public class ProdutoServiceTest
    {
        private readonly MockRepository _mocks = new(MockBehavior.Strict);

        Mock<IProdutoRepository> _produtoRepository;

        [SetUp]
        public void SetUP()
        {
            _produtoRepository = _mocks.Create<IProdutoRepository>(MockBehavior.Strict);
        }

        [Test]
        public async Task ProdutoService_AdicionarAsync_Sucesso()
        {
            // Arrange
            ProdutoRequest request = new() { Ativo = true, Nome = "Produto 1", IdCategoria = Guid.NewGuid(), Preco = 9.99m };

            _produtoRepository
                .Setup(x => x.AdicionarAsync(It.Is<ProdutoModel>(t => t.Id.Equals(request.Id))))
                .Returns(Task.CompletedTask);

            ProdutoService service = GetService();

            // Act
            ProdutoResponse response = await service.AdicionarAsync(request);

            // Assert
            Assert.IsNotNull(response);
            _mocks.Verify();
        }

        [Test]
        public async Task ProdutoService_AtualizarAsync_Sucesso()
        {
            // Arrange

            ProdutoRequest request = new() { Ativo = true, Nome = "Produto 1", IdCategoria = Guid.NewGuid(), Preco = 9.99m };

            _produtoRepository.Setup(x => x.AlterarAsync(It.Is<ProdutoModel>(t => t.Id.Equals(request.Id))))
                .Returns(Task.CompletedTask);

            ProdutoService service = GetService();

            // Act
            ProdutoResponse result = await service.AlterarAsync(request);

            // Assert
            Assert.IsNotNull(result);
            _mocks.Verify();
        }

        private ProdutoService GetService()
            => new(_produtoRepository.Object);
    }
}
