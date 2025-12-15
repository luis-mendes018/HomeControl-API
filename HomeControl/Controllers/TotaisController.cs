using Application.DTOs.TotalCategoria;
using Application.DTOs.TotalUsuario;
using Application.Services;

using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

namespace HomeControl.Controllers;

[Route("api/v{version:apiVersion}/totais")]
[ApiController]
[ApiVersion("1.0")]
public class TotaisController : ControllerBase
{
    private readonly TransacaoService _transacaoService;

    public TotaisController(TransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

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
