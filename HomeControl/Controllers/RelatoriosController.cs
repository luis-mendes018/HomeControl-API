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
