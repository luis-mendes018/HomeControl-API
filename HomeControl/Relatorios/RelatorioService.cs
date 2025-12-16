using Application.Interfaces;
using Application.Services;

using QuestPDF.Fluent;

namespace HomeControl.Relatorios;

/// <summary>
/// Implementação do serviço de relatórios.
/// A geração do PDF é feita nesta camada pois depende
/// diretamente da biblioteca QuestPDF.
/// </summary>
public class RelatorioService : IRelatorioService
{
    private readonly TransacaoService _transacaoService;

    public RelatorioService(TransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    public byte[] GerarTotaisPorUsuarioPdf()
    {
        var dados = _transacaoService.ObterTotaisPorUsuarioAsync().Result;

        var document = new TotaisPorUsuarioDocument(dados);
        return document.GeneratePdf();
    }

    public byte[] GerarTotaisPorCategoriaPdf()
    {
        var dados = _transacaoService.ObterTotaisPorCategoriaAsync().Result;

        var document = new TotaisPorCategoriaDocument(dados);
        return document.GeneratePdf();
    }

}
