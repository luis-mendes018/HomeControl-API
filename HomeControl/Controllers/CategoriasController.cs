using Application.DTOs.Categoria;

using Asp.Versioning;

using Domain.Entities;
using Domain.Interfaces;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using Nelibur.ObjectMapper;

namespace HomeControl.Controllers;

[Route("api/v{version:apiVersion}/categorias")]
[ApiController]
[ApiVersion("1.0")]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CategoriaCreateDto> _createValidator;
    private readonly IValidator<CategoriaUpdateDto> _updateValidator;

    public CategoriasController(IUnitOfWork unitOfWork, 
        IValidator<CategoriaUpdateDto> updateValidator, 
        IValidator<CategoriaCreateDto> createValidator)
    {
        _unitOfWork = unitOfWork;
        _updateValidator = updateValidator;
        _createValidator = createValidator;
    }

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


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoriaResponseDto>> GetById(Guid id)
    {
        
        var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(id);

        if (categoria == null)
            return NotFound();

        
        var dto = TinyMapper.Map<CategoriaResponseDto>(categoria);

        return Ok(dto);
    }

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

        
        categoria.Atualizar(categoriaUpdateDto.Descricao, categoriaUpdateDto.Finalidade);

        await _unitOfWork.CommitAsync();

        var responseDto = TinyMapper.Map<CategoriaResponseDto>(categoria);
        return Ok(responseDto);
    }



    [HttpDelete("excluir/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(id);

        if (categoria is null)
            return NotFound();

        await _unitOfWork.CategoriaRepository.DeleteAsync(categoria);
        await _unitOfWork.CommitAsync();

        return NoContent();
    }

}
