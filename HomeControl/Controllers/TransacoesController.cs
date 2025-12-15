using Application.DTOs.Transacao;
using Asp.Versioning;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;

namespace HomeControl.Controllers;

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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TransacaoResponseDto>> GetById(Guid id)
    {
        var transacao = await _unitOfWork.TransacaoRepository.GetByIdAsync(id);

        if (transacao == null)
            return NotFound();

        var dto = TinyMapper.Map<TransacaoResponseDto>(transacao);
        return Ok(dto);
    }

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


    [HttpPost("nova")]
    public async Task<ActionResult<TransacaoResponseDto>> Create([FromBody] TransacaoCreateDto dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        // 1. Buscar usuário
        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(dto.UsuarioId);
        if (usuario == null)
            return BadRequest("Usuário não encontrado.");

        // 2. Buscar categoria
        var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(dto.CategoriaId);
        if (categoria == null)
            return BadRequest("Categoria não encontrada.");

        // 3. Regras de negócio
        if (usuario.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
            return BadRequest("Menores de idade não podem registrar receitas.");

        if ((dto.Tipo == TipoTransacao.Despesa && categoria.Finalidade != FinalidadeCategoria.Despesa) ||
            (dto.Tipo == TipoTransacao.Receita && categoria.Finalidade != FinalidadeCategoria.Receita))
            return BadRequest("Categoria incompatível com o tipo da transação.");

        // 4. Criar transação
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
