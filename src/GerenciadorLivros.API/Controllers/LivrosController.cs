using GerenciadorLivros.Domain.Entities;
using GerenciadorLivros.Service.DTOs;
using GerenciadorLivros.Service.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorLivros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly ILivroService _service;

    public LivrosController(ILivroService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Livro>>> Get()
    {
        var livros = await _service.ObterTodosAsync();
        return Ok(livros);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Livro>> Get(Guid id)
    {
        var livro = await _service.ObterPorIdAsync(id);
        if (livro == null) return NotFound("Livro não encontrado.");

        return Ok(livro);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] LivroCreateDto livro)
    {
        var id = await _service.AdicionarAsync(livro);
        return CreatedAtAction(nameof(Get), new { id }, livro);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] LivroUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _service.AtualizarAsync(id, dto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Livro não encontrado.");
        }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.ExcluirAsync(id);
        return NoContent();
    }

    [HttpPatch("{id:guid}/marcar-lido")]
    public async Task<ActionResult> MarcarComoLido(Guid id)
    {
        var sucesso = await _service.MarcarComoLidoAsync(id);
        if (!sucesso) return NotFound("Livro não encontrado.");

        return Ok("Livro atualizado com sucesso!");
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<LivroPatchDto> patchDoc)
    {
        if (patchDoc == null) return BadRequest("Patch document is required.");

        var dto = new LivroPatchDto();
        patchDoc.ApplyTo(dto, ModelState);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _service.PatchAsync(id, dto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Livro não encontrado.");
        }

        return NoContent();
    }
}