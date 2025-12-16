using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using Moq;

namespace Application.Tests.Services;

public class TransacaoServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ITransacaoRepository> _transacaoRepoMock;
    private readonly TransacaoService _transacaoService;

    public TransacaoServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _transacaoRepoMock = new Mock<ITransacaoRepository>();

        // Configura UnitOfWork para retornar o repositório mockado
        _unitOfWorkMock.Setup(u => u.TransacaoRepository).Returns(_transacaoRepoMock.Object);

        _transacaoService = new TransacaoService(_unitOfWorkMock.Object);
    }


    [Fact]
    public async Task ObterTotaisPorUsuarioAsync_DeveRetornarDtosCorretamente()
    {
        // Arrange: mock do retorno do repositório
        var mockResult = new List<UsuarioTotais>
        {
            new UsuarioTotais
            {
                UsuarioId = Guid.NewGuid(),
                Nome = "Maria",
                TotalReceita = 1000,
                TotalDespesa = 400
            }
        };

        // Configura o mock do repositório
        _transacaoRepoMock
            .Setup(r => r.ObterTotaisPorUsuarioAsync())
            .ReturnsAsync(mockResult);  // <- aqui

        // Act
        var result = await _transacaoService.ObterTotaisPorUsuarioAsync();

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(1);
        result.First().Nome.Should().Be("Maria");
        result.First().TotalReceita.Should().Be(1000);
        result.First().TotalDespesa.Should().Be(400);

    }



    [Fact]
    public async Task ObterTotaisPorCategoriaAsync_DeveRetornarDtosCorretamente()
    {
        // Arrange: mock do retorno do repositório (tipo real do domínio)
        var mockResult = new List<CategoriaTotais>
    {
        new CategoriaTotais
        {
            CategoriaId = Guid.NewGuid(),
            Nome = "Alimentação",
            TotalReceita = 500,
            TotalDespesa = 300
        }
    };

        _transacaoRepoMock
            .Setup(r => r.ObterTotaisPorCategoriaAsync())
            .ReturnsAsync(mockResult);

        // Act
        var result = await _transacaoService.ObterTotaisPorCategoriaAsync();

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(1);
        var primeiraCategoria = result.First();
        primeiraCategoria.Nome.Should().Be("Alimentação");
        primeiraCategoria.TotalReceita.Should().Be(500);
        primeiraCategoria.TotalDespesa.Should().Be(300);
        primeiraCategoria.CategoriaId.Should().Be(mockResult.First().CategoriaId);
    }

}
