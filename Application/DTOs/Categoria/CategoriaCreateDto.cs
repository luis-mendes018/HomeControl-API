using Domain.Enums;

namespace Application.DTOs.Categoria;

public class CategoriaCreateDto
{
    public string Descricao { get; set; }
    public FinalidadeCategoria Finalidade { get; set; }
}
