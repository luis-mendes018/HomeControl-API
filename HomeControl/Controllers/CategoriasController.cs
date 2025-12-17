using Application.DTOs.Categoria;

using Asp.Versioning;

using Domain.Entities;
using Domain.Interfaces;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using Nelibur.ObjectMapper;

namespace HomeControl.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de categorias.
/// </summary>
/// <remarks>
/// Fornece operações de listagem, busca, criação, atualização
/// e exclusão de categorias, com suporte a paginação e validação.
/// </remarks>
/// 
[Route("api/v{version:apiVersion}/categorias")]
[ApiController]
[ApiVersion("1.0")]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CategoriaCreateDto> _createValidator;
    private readonly IValidator<CategoriaUpdateDto> _updateValidator;

    /// <summary>
    /// Inicializa o controller de categorias com suas dependências.
    /// </summary>
    /// <param name="unitOfWork">
    /// Unidade de trabalho responsável pelo acesso aos repositórios
    /// e controle de transações.
    /// </param>
    /// <param name="updateValidator">Validador do DTO de atualização.</param>
    /// <param name="createValidator">Validador do DTO de criação.</param>
    public CategoriasController(IUnitOfWork unitOfWork, 
        IValidator<CategoriaUpdateDto> updateValidator, 
        IValidator<CategoriaCreateDto> createValidator)
    {
        _unitOfWork = unitOfWork;
        _updateValidator = updateValidator;
        _createValidator = createValidator;
    }


    /// <summary>
    /// Retorna uma lista paginada de categorias.
    /// </summary>
    /// <remarks>
    /// Os metadados de paginação são retornados nos headers da resposta.
    /// </remarks>
    /// <response code="200">Lista de categorias retornada com sucesso.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaResponseDto>>> GetAll(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        var pagedResult = await _unitOfWork.CategoriaRepository.GetPagedAsync(pageNumber, pageSize);

        Response.Headers["X-PageNumber"] = pagedResult.PageNumber.ToString();
        Response.Headers["X-PageSize"] = pagedResult.PageSize.ToString();
        Response.Headers["X-TotalRecords"] = pagedResult.TotalRecords.ToString();
        Response.Headers["X-TotalPages"] = pagedResult.TotalPages.ToString();

       
        var dtoList = pagedResult.Items.Select(TinyMapper.Map<CategoriaResponseDto>);

        return Ok(dtoList);
    }


    /// <summary>
    /// Retorna uma categoria específica pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador único da categoria.</param>
    /// <response code="200">Categoria encontrada.</response>
    /// <response code="404">Categoria não encontrada.</response>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoriaResponseDto>> GetById(Guid id)
    {
        
        var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(id);

        if (categoria == null)
            return NotFound();

        
        var dto = TinyMapper.Map<CategoriaResponseDto>(categoria);

        return Ok(dto);
    }


    /// <summary>
    /// Realiza a busca de categorias por descrição, com paginação.
    /// </summary>
    /// <param name="descricao">Texto utilizado para filtrar categorias.</param>
    /// <param name="pageNumber">Número da página.</param>
    /// <param name="pageSize">Quantidade de registros por página.</param>
    /// <remarks>
    /// A busca é realizada por correspondência parcial do nome.
    /// </remarks>
    /// <response code="200">Categorias encontradas.</response>
    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<CategoriaResponseDto>>> BuscarPorDescricao(
    [FromQuery] string descricao,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        var pagedResult = await _unitOfWork
            .CategoriaRepository
            .BuscarPorNomeAsync(descricao, pageNumber, pageSize);

        Response.Headers["X-PageNumber"] = pagedResult.PageNumber.ToString();
        Response.Headers["X-PageSize"] = pagedResult.PageSize.ToString();
        Response.Headers["X-TotalRecords"] = pagedResult.TotalRecords.ToString();
        Response.Headers["X-TotalPages"] = pagedResult.TotalPages.ToString();

        var dtoList = pagedResult.Items
            .Select(c => TinyMapper.Map<CategoriaResponseDto>(c));

        return Ok(dtoList);
    }



    /// <summary>
    /// Cria uma nova categoria.
    /// </summary>
    /// <remarks>
    /// Os dados de entrada são validados antes da persistência.
    /// </remarks>
    /// <response code="201">Categoria criada com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>

    [HttpPost("criar")]
    public async Task<ActionResult<CategoriaResponseDto>> Create(
        [FromBody] CategoriaCreateDto categoriaCreateDto)
    {
        var validationResult = await _createValidator.ValidateAsync(categoriaCreateDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        var categoria = new Categoria(categoriaCreateDto.Descricao, categoriaCreateDto.Finalidade);

        await _unitOfWork.CategoriaRepository.AddAsync(categoria);
        await _unitOfWork.CommitAsync();

        var responseDto = TinyMapper.Map<CategoriaResponseDto>(categoria);

        return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, responseDto);
    }


    /// <summary>
    /// Atualiza os dados de uma categoria existente.
    /// </summary>
    /// <param name="id">Identificador da categoria.</param>
    /// <param name="categoriaUpdateDto">Dados atualizados da categoria.</param>
    /// <response code="200">Categoria atualizada com sucesso.</response>
    /// <response code="404">Categoria não encontrada.</response>

    [HttpPut("atualizar/{id:guid}")]
    public async Task<ActionResult<CategoriaResponseDto>> Update(
    Guid id,
    [FromBody] CategoriaUpdateDto categoriaUpdateDto)
    {
        var validationResult = await _updateValidator.ValidateAsync(categoriaUpdateDto);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return BadRequest(ModelState);
        }

        var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(id);
        if (categoria == null) return NotFound();

        
        categoria.Atualizar(categoriaUpdateDto.Descricao);

        await _unitOfWork.CommitAsync();

        var responseDto = TinyMapper.Map<CategoriaResponseDto>(categoria);
        return Ok(responseDto);
    }

}
