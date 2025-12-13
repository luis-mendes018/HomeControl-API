using Domain.Enums;

namespace Application.DTOs.Categoria;

public class CategoriaUpdateDto
{
    public string Descricao { get; set; }
    public FinalidadeCategoria Finalidade { get; set; }
}
