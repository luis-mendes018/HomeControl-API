using Application.Interfaces;
using Application.Services;

using QuestPDF.Fluent;

namespace HomeControl.Relatorios;

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
