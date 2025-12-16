using Application.DTOs.Transacao;
using Asp.Versioning;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;

namespace HomeControl.Controllers;


/// <summary>
/// Controlador responsável pelo gerenciamento de transações financeiras.
/// Permite consulta, busca e criação de transações, aplicando regras de negócio.
/// </summary>
[Route("api/v{version:apiVersion}/transacoes")]
[ApiController]
[ApiVersion("1.0")]
public class TransacoesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<TransacaoCreateDto> _createValidator;

    public TransacoesController(IUnitOfWork unitOfWork, 
        IValidator<TransacaoCreateDto> createValidator)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
    }

    /// <summary>
    /// Retorna uma lista paginada de transações.
    /// </summary>
    /// <param name="pageNumber">Número da página.</param>
    /// <param name="pageSize">Quantidade de registros por página.</param>
    /// <returns>Lista de transações.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransacaoResponseDto>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var pagedResult = await _unitOfWork.TransacaoRepository.GetPagedAsync(pageNumber, pageSize);
        Response.Headers["X-PageNumber"] = pagedResult.PageNumber.ToString();
        Response.Headers["X-PageSize"] = pagedResult.PageSize.ToString();
        Response.Headers["X-TotalRecords"] = pagedResult.TotalRecords.ToString();
        Response.Headers["X-TotalPages"] = pagedResult.TotalPages.ToString();
        var dtoList = pagedResult.Items.Select(TinyMapper.Map<TransacaoResponseDto>);
        
        return Ok(dtoList);
    }

    /// <summary>
    /// Retorna uma transação específica pelo identificador.
    /// </summary>
    /// <param name="id">Id da transação.</param>
    /// <returns>Transação encontrada ou 404.</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TransacaoResponseDto>> GetById(Guid id)
    {
        var transacao = await _unitOfWork.TransacaoRepository.GetByIdAsync(id);

        if (transacao == null)
            return NotFound();

        var dto = TinyMapper.Map<TransacaoResponseDto>(transacao);
        return Ok(dto);
    }

    /// <summary>
    /// Busca transações por descrição ou código, com paginação.
    /// </summary>
    /// <param name="filtro">Texto para busca.</param>
    /// <param name="pageNumber">Número da página.</param>
    /// <param name="pageSize">Quantidade de registros.</param>
    /// <returns>Lista de transações filtradas.</returns>
    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<TransacaoResponseDto>>> BuscarPorDescricao(
        [FromQuery] string filtro,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var pagedResult = await _unitOfWork.TransacaoRepository.BuscarTransacaoPorNomeOuCodigoAsync(filtro, pageNumber, pageSize);

        Response.Headers["X-PageNumber"] = pagedResult.PageNumber.ToString();
        Response.Headers["X-PageSize"] = pagedResult.PageSize.ToString();
        Response.Headers["X-TotalRecords"] = pagedResult.TotalRecords.ToString();
        Response.Headers["X-TotalPages"] = pagedResult.TotalPages.ToString();
        var dtoList = pagedResult.Items.Select(TinyMapper.Map<TransacaoResponseDto>);
        
        return Ok(dtoList);
    }


    /// <summary>
    /// Cria uma nova transação financeira.
    /// </summary>
    /// <remarks>
    /// Regras aplicadas:
    /// - Usuário deve existir.
    /// - Categoria deve existir.
    /// - Menores de idade não podem registrar receitas.
    /// - A finalidade da categoria deve ser compatível com o tipo da transação.
    /// </remarks>
    /// <param name="dto">Dados da transação.</param>
    /// <returns>Transação criada ou erro de validação.</returns>
    [HttpPost("nova")]
    public async Task<ActionResult<TransacaoResponseDto>> Create([FromBody] TransacaoCreateDto dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        
        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(dto.UsuarioId);
        if (usuario == null)
            return BadRequest("Usuário não encontrado.");

       
        var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(dto.CategoriaId);
        if (categoria == null)
            return BadRequest("Categoria não encontrada.");

        if (usuario.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
            return BadRequest("Menores de idade não podem registrar receitas.");

        if ((dto.Tipo == TipoTransacao.Despesa && categoria.Finalidade != FinalidadeCategoria.Despesa) ||
            (dto.Tipo == TipoTransacao.Receita && categoria.Finalidade != FinalidadeCategoria.Receita))
            return BadRequest("Categoria incompatível com o tipo da transação.");

        
        var transacao = new Transacao(
            dto.Valor,
            dto.Descricao,
            categoria,
            dto.Tipo,
            usuario
        );

        await _unitOfWork.TransacaoRepository.AddAsync(transacao);
        await _unitOfWork.CommitAsync();

        var response = TinyMapper.Map<TransacaoResponseDto>(transacao);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

}
