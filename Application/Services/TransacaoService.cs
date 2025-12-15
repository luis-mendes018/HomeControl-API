using Application.DTOs.TotalCategoria;
using Application.DTOs.TotalUsuario;

using Domain.Interfaces;

namespace Application.Services;

public class TransacaoService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransacaoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IEnumerable<TotaisPorUsuarioDto>> ObterTotaisPorUsuarioAsync()
    {
        var resultados = await _unitOfWork.TransacaoRepository.ObterTotaisPorUsuarioAsync();
        return resultados.Select(r => new TotaisPorUsuarioDto
        {
            UsuarioId = r.UsuarioId,
            Nome = r.Nome,
            TotalReceita = r.TotalReceita,
            TotalDespesa = r.TotalDespesa
        });
    }
    public async Task<IEnumerable<TotaisPorCategoriaDto>> ObterTotaisPorCategoriaAsync()
    {
        var resultados = await _unitOfWork.TransacaoRepository.ObterTotaisPorCategoriaAsync();
        return resultados.Select(r => new TotaisPorCategoriaDto
        {
            CategoriaId = r.CategoriaId,
            Nome = r.Nome,
            TotalReceita = r.TotalReceita,
            TotalDespesa = r.TotalDespesa
        });
    }

}
