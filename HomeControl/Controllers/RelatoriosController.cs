using Application.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace HomeControl.Controllers;

[Route("api/v{verion:apiVersion}/relatorios")]
[ApiController]
[ApiVersion("1.0")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _relatorioService;

    public RelatoriosController(IRelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }


    /// <summary>
    /// Retorna o relatório de totais financeiros agrupados por usuário.
    /// </summary>
    /// <response code="200">PDF gerado com sucesso.</response>
    /// 
    [HttpGet("totais-por-usuario")]
    public IActionResult GerarRelatorioTotaisPorUsuario()
    {
        var pdfBytes = _relatorioService.GerarTotaisPorUsuarioPdf();

        return File(
            pdfBytes,
            "application/pdf",
            "totais-por-usuario.pdf"
        );
    }


    /// <summary>
    /// Gera um relatório financeiro em PDF contendo os totais
    /// agrupados por categoria.
    /// </summary>
    /// <remarks>
    /// O relatório é gerado dinamicamente utilizando QuestPDF
    /// e retorna o arquivo para download.
    /// </remarks>
    /// <response code="200">PDF gerado com sucesso.</response>
    [HttpGet("totais-por-categoria")]
    public IActionResult GerarRelatorioTotaisPorCategoria()
    {
        var pdfBytes = _relatorioService.GerarTotaisPorCategoriaPdf();

        return File(
            pdfBytes,
            "application/pdf",
            "totais-por-categoria.pdf"
        );
    }
}
