using Application.DTOs.Categoria;
using Application.DTOs.Usuario;

using Domain.Enums;

using System.Text.Json.Serialization;

namespace Application.DTOs.Transacao;

/// <summary>
/// DTO de resposta que representa uma transação financeira.
/// </summary>
public class TransacaoResponseDto
{
    /// <summary>
    /// Identificador único da transação.
    /// </summary>
    /// <example>9c1a3f8d-4b7a-4e5c-9c8f-2a1b3d4e5f6a</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Código sequencial da transação.
    /// </summary>
    /// <example>1024</example>
    public int CodigoTransacao { get; set; }

    /// <summary>
    /// Valor da transação.
    /// </summary>
    /// <example>350.00</example>
    public decimal Valor { get; set; }

    /// <summary>
    /// Descrição da transação.
    /// </summary>
    /// <example>Pagamento de fornecedor</example>
    public string Descricao { get; set; }
    
    /// <summary>
    /// Tipo da transação.
    /// </summary>
    /// <example>Pagamento de fornecedor</example>
    public TipoTransacao Tipo { get; set; }

    /// <summary>
    /// Data e hora de criação da transação.
    /// Campo interno não exposto diretamente na resposta JSON.
    /// </summary>
    [JsonIgnore]
    public DateTime DataCriacao { get; set; }

    /// <summary>
    /// Data e hora de criação da transação formatada para exibição.
    /// </summary>
    /// <example>15/12/2025 14:30</example>
    public string DataCriacaoDto => DataCriacao.ToString("dd/MM/yyyy HH:mm");

    /// <summary>
    /// Identificador da categoria associada à transação.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid CategoriaId { get; set; }

    /// <summary>
    /// Dados da categoria associada à transação.
    /// </summary>
    public CategoriaResponseDto Categoria { get; set; }

    /// <summary>
    /// Identificador do usuário responsável pela transação.
    /// </summary>
    /// <example>8b5d9e62-1b42-4f77-9a9f-4f6d2b8c1234</example>
    public Guid UsuarioId { get; set; }

    /// <summary>
    /// Dados do usuário responsável pela transação.
    /// </summary>
    public UsuarioResponseDto Usuario { get; set; } 
}
