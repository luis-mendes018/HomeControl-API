using Application.DTOs.Categoria;
using Application.DTOs.Transacao;
using Application.DTOs.Usuario;

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

        // Transacao    
        TinyMapper.Bind<TransacaoCreateDto, Transacao>();
        TinyMapper.Bind<Transacao, TransacaoResponseDto>();

        //Usuario
        TinyMapper.Bind<UsuarioCreateDto, Usuario>();
        TinyMapper.Bind<Usuario, UsuarioResponseDto>();
    }
}
