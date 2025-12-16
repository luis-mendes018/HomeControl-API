using Application.DTOs.TotalCategoria;
using Application.DTOs.TotalUsuario;
using Application.Services;

using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

namespace HomeControl.Controllers;


/// <summary>
/// Controller responsável por expor endpoints de consulta
/// dos totais financeiros consolidados.
/// </summary>
/// <remarks>
/// Os dados retornados são utilizados tanto para exibição
/// em tela quanto para geração de relatórios em PDF.
/// </remarks>

[Route("api/v{version:apiVersion}/totais")]
[ApiController]
[ApiVersion("1.0")]
public class TotaisController : ControllerBase
{
    private readonly TransacaoService _transacaoService;

    /// <summary>
    /// Inicializa o controller de totais com as dependências necessárias.
    /// </summary>
    /// <param name="transacaoService">
    /// Serviço responsável por consolidar os valores financeiros.
    /// </param>
    public TotaisController(TransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }


    /// <summary>
    /// Retorna os totais financeiros agrupados por usuário.
    /// </summary>
    /// <remarks>
    /// Além dos valores individuais por usuário, o endpoint
    /// adiciona uma linha de total geral ao final da lista.
    /// </remarks>
    /// <response code="200">Lista de totais por usuário retornada com sucesso.</response>
    [HttpGet("totais-por-usuario")]
    public async Task<ActionResult<IEnumerable<TotaisPorUsuarioDto>>> TotaisPorUsuario()
    {
        var resultado = await _transacaoService.ObterTotaisPorUsuarioAsync();

        // Calculando total geral
        var totalGeral = new TotaisPorUsuarioDto
        {
            UsuarioId = null,
            Nome = "Total Geral",
            TotalReceita = resultado.Sum(r => r.TotalReceita),
            TotalDespesa = resultado.Sum(r => r.TotalDespesa)
        };

        return Ok(resultado.Append(totalGeral));
    }


    /// <summary>
    /// Retorna os totais financeiros agrupados por categoria.
    /// </summary>
    /// <remarks>
    /// O resultado inclui os totais por categoria e
    /// uma linha adicional representando o total geral.
    /// </remarks>
    /// <response code="200">Lista de totais por categoria retornada com sucesso.</response>
    [HttpGet("totais-por-categoria")]
    public async Task<ActionResult<IEnumerable<TotaisPorCategoriaDto>>> TotaisPorCategoria()
    {
        var resultado = await _transacaoService.ObterTotaisPorCategoriaAsync();

        // Calculando total geral
        var totalGeral = new TotaisPorCategoriaDto
        {
            CategoriaId= null,
            Nome = "Total Geral",
            TotalReceita = resultado.Sum(r => r.TotalReceita),
            TotalDespesa = resultado.Sum(r => r.TotalDespesa)
        };

        return Ok(resultado.Append(totalGeral));
    }
}
