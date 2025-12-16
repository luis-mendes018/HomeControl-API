namespace Application.Interfaces;

/// <summary>
/// Responsável por gerar relatórios financeiros em formato PDF.
/// </summary>
public interface IRelatorioService
{
    byte[] GerarTotaisPorUsuarioPdf();
    byte[] GerarTotaisPorCategoriaPdf();
}
