using Application.DTOs.Categoria;

using Domain.Entities;

using Nelibur.ObjectMapper;

namespace Application.Mappings;

public static class TinyMapperConfig
{
    public static void Register()
    {
        // Categoria
        TinyMapper.Bind<CategoriaCreateDto, Categoria>();
        TinyMapper.Bind<Categoria, CategoriaResponseDto>();
    }
}
