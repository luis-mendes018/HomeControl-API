using Application.DTOs.Usuario;

using Asp.Versioning;

using Domain.Entities;
using Domain.Interfaces;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using Nelibur.ObjectMapper;

namespace HomeControl.Controllers;

[Route("api/v{version:apiVersion}/usuarios")]
[ApiController]
[ApiVersion("1.0")]
public class UsuariosController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UsuarioCreateDto> _createValidator;
    private readonly IValidator<UsuarioUpdateDto> _updateValidator;
    public UsuariosController(IUnitOfWork unitOfWork, IValidator<UsuarioCreateDto> createValidator, IValidator<UsuarioUpdateDto> updateValidator)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var pagedResult = await _unitOfWork.UsuarioRepository.GetPagedAsync(pageNumber, pageSize);

        Response.Headers["X-PageNumber"] = pagedResult.PageNumber.ToString();
        Response.Headers["X-PageSize"] = pagedResult.PageSize.ToString();
        Response.Headers["X-TotalRecords"] = pagedResult.TotalRecords.ToString();
        Response.Headers["X-TotalPages"] = pagedResult.TotalPages.ToString();

        var dtoList = pagedResult.Items.Select(TinyMapper.Map<UsuarioResponseDto>);
        
        return Ok(dtoList);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UsuarioResponseDto>> GetById(Guid id)
    {
        
        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound();
        
        var dto = TinyMapper.Map<UsuarioResponseDto>(usuario);
        return Ok(dto);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> BuscarPorNome(
        [FromQuery] string nome,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var pagedResult = await _unitOfWork.UsuarioRepository
            .BuscarPorNomeUsuarioAsync(nome, pageNumber, pageSize);
        Response.Headers["X-PageNumber"] = pagedResult.PageNumber.ToString();
        Response.Headers["X-PageSize"] = pagedResult.PageSize.ToString();
        Response.Headers["X-TotalRecords"] = pagedResult.TotalRecords.ToString();
        Response.Headers["X-TotalPages"] = pagedResult.TotalPages.ToString();
        var dtoList = pagedResult.Items.Select(TinyMapper.Map<UsuarioResponseDto>);
        
        return Ok(dtoList);
    }

    [HttpPost("cadastrar")]
    public async Task<ActionResult<UsuarioResponseDto>> Create(
        [FromBody] UsuarioCreateDto usuarioCreateDto)
    {
        var validationResult = await _createValidator.ValidateAsync(usuarioCreateDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        var usuario = new Usuario(usuarioCreateDto.Nome, usuarioCreateDto.Idade);

        await _unitOfWork.UsuarioRepository.AddAsync(usuario);
        await _unitOfWork.CommitAsync();

        var usuarioResponseDto = TinyMapper.Map<UsuarioResponseDto>(usuario);

        return CreatedAtAction(nameof(GetById), new { id = usuarioResponseDto.Id }, usuarioResponseDto);
    }

    [HttpPut("atualizar/{id:guid}")]
    public async Task <ActionResult<UsuarioResponseDto>> Update(
        Guid id, 
        [FromBody] UsuarioUpdateDto usuarioUpdateDto)
    {
        var validationResult = await _updateValidator.ValidateAsync(usuarioUpdateDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound();

        usuario.Atualizar(usuarioUpdateDto.Nome, usuarioUpdateDto.Idade);

        await _unitOfWork.CommitAsync();

        var usuarioResponseDto = TinyMapper.Map<UsuarioResponseDto>(usuario);
        return Ok(usuarioResponseDto);
    }


    [HttpDelete("excluir/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound();

        await _unitOfWork.UsuarioRepository.DeleteAsync(usuario);
        await _unitOfWork.CommitAsync();

        return NoContent();
    }

}
