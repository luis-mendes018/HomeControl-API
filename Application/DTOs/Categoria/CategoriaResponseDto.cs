using Domain.Enums;

namespace Application.DTOs.Categoria;

public class CategoriaResponseDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public FinalidadeCategoria Finalidade { get; set; }
}
