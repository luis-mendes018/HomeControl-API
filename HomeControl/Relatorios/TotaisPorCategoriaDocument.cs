using Application.DTOs.TotalCategoria;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HomeControl.Relatorios;

/// <summary>
/// Documento QuestPDF responsável pela renderização
/// do relatório de totais financeiros agrupados por categoria.
/// </summary>
/// <remarks>
/// Contém tabela com valores individuais e total geral ao final.
/// </remarks>
public class TotaisPorCategoriaDocument : IDocument
{
    private readonly IEnumerable<TotaisPorCategoriaDto> _dados;

    public TotaisPorCategoriaDocument(IEnumerable<TotaisPorCategoriaDto> dados)
    {
        _dados = dados;
    }

    public DocumentMetadata GetMetadata() =>
        DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        // Estrutura o relatório em uma tabela contendo:
        // - valores por categoria
        // - total geral ao final
        var totalReceitas = _dados.Sum(x => x.TotalReceita);
        var totalDespesas = _dados.Sum(x => x.TotalDespesa);
        var saldoGeral = totalReceitas - totalDespesas;

        container.Page(page =>
        {
            page.Margin(40);

            page.Content().Column(column =>
            {
                
                column.Item().Text("Relatório de Despesas e Receitas Totais por Categoria")
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

                    
                    table.Header(header =>
                    {
                        header.Cell().Element(CellHeaderStyle).Text("Categoria");
                        header.Cell().Element(CellHeaderStyle).AlignRight().Text("Receitas");
                        header.Cell().Element(CellHeaderStyle).AlignRight().Text("Despesas");
                        header.Cell().Element(CellHeaderStyle).AlignRight().Text("Saldo");
                    });

                    
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
