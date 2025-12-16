using Application.DTOs.TotalUsuario;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HomeControl.Relatorios;

/// <summary>
/// Documento QuestPDF responsável pela renderização
/// do relatório de totais financeiros agrupados por usuário.
/// </summary>
/// <remarks>
/// Contém tabela com valores individuais e total geral ao final.
/// </remarks>
public class TotaisPorUsuarioDocument : IDocument
{
    private readonly IEnumerable<TotaisPorUsuarioDto> _dados;

    public TotaisPorUsuarioDocument(IEnumerable<TotaisPorUsuarioDto> dados)
    {
        _dados = dados;
    }

    public DocumentMetadata GetMetadata() =>
        DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        // Cálculo do total geral
        var totalReceitas = _dados.Sum(x => x.TotalReceita);
        var totalDespesas = _dados.Sum(x => x.TotalDespesa);
        var saldoGeral = totalReceitas - totalDespesas;

        container.Page(page =>
        {
            page.Margin(40);

            page.Content().Column(column =>
            {
                
                column.Item().Text("Relatório do Total de Despesas e Receitas por Usuário")
                    .FontSize(20)
                    .Bold();

                column.Item().PaddingVertical(15);

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3); 
                        columns.RelativeColumn(2); 
                        columns.RelativeColumn(2); 
                        columns.RelativeColumn(2); 
                    });

                    // Cabeçalho
                    table.Header(header =>
                    {
                        header.Cell().Element(CellHeaderStyle).Text("Nome");
                        header.Cell().Element(CellHeaderStyle).AlignRight().Text("Receitas");
                        header.Cell().Element(CellHeaderStyle).AlignRight().Text("Despesas");
                        header.Cell().Element(CellHeaderStyle).AlignRight().Text("Saldo");
                    });

                    // Linhas
                    for (int i = 0; i < _dados.Count(); i++)
                    {
                        var item = _dados.ElementAt(i);
                        var background = i % 2 == 0 ? Colors.Grey.Lighten3 : Colors.White;

                        table.Cell().Element(c => CellBodyStyle(c, background))
                            .Text(item.Nome);

                        table.Cell().Element(c => CellBodyStyle(c, background))
                            .AlignRight().Text(item.TotalReceita.ToString("C2"));

                        table.Cell().Element(c => CellBodyStyle(c, background))
                            .AlignRight().Text(item.TotalDespesa.ToString("C2"));

                        table.Cell().Element(c => CellBodyStyle(c, background))
                            .AlignRight()
                            .Text(item.Saldo.ToString("C2"))
                            .Bold();
                    }

                    // LINHA TOTAL GERAL
                    table.Cell().Element(CellHeaderStyle)
                        .Text("TOTAL GERAL");

                    table.Cell().Element(CellHeaderStyle)
                        .AlignRight().Text(totalReceitas.ToString("C2"));

                    table.Cell().Element(CellHeaderStyle)
                        .AlignRight().Text(totalDespesas.ToString("C2"));

                    table.Cell().Element(CellHeaderStyle)
                        .AlignRight()
                        .Text(saldoGeral.ToString("C2"));
                });
            });
        });
    }



    static IContainer CellHeaderStyle(IContainer container)
    {
        return container
            .Background(Colors.Grey.Lighten2)
            .Border(1)
            .BorderColor(Colors.Grey.Medium)
            .Padding(8)
            .DefaultTextStyle(x => x.SemiBold());
    }

    static IContainer CellBodyStyle(IContainer container, string backgroundColor)
    {
        return container
            .Background(backgroundColor)
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten2)
            .Padding(6);
    }

}
