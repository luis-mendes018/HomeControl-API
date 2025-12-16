using Application.DTOs.Categoria;
using Application.DTOs.Transacao;
using Application.DTOs.Usuario;

using Domain.Entities;

using Nelibur.ObjectMapper;

namespace Application.Mappings;

/// <summary>
/// Classe responsável por registrar os mapeamentos entre DTOs e entidades
/// utilizando o TinyMapper.
///
/// Centraliza todas as configurações de mapeamento do sistema,
/// evitando mapeamentos espalhados pelo código.
/// </summary>
/// <remarks>
/// Regras importantes:
/// - DTOs de criação mapeiam para entidades de domínio
/// - Entidades de domínio mapeiam para DTOs de resposta
/// - DTOs de atualização devem ser aplicados manualmente ou via serviço
/// - Não adicionar regras de negócio ou lógica condicional aqui
/// </remarks>
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
