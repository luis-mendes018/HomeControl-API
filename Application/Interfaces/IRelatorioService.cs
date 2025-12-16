namespace Application.Interfaces;

public interface IRelatorioService
{
    byte[] GerarTotaisPorUsuarioPdf();
    byte[] GerarTotaisPorCategoriaPdf();
}
