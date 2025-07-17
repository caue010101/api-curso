using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("curso")]

public class CursoController : ControllerBase
{
    private readonly ICursoRepository _repository;

    public CursoController(ICursoRepository repository)
    {
        this._repository = repository;
    }

    [HttpGet("BuscarCursoPorId/{id}", Name = "BuscarCursoPorId")]

    public async Task<ActionResult<CursoDTO>> BuscarCursoPorIdAsync(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { mensagem = "Modelo invalido " });
        }

        var cursoEncontrar = await _repository.ObterCursoPorIdAsync(id);

        if (cursoEncontrar is null)
        {
            return NotFound(new { mensagem = "Curso nao encontrado " });
        }

        return Ok(cursoEncontrar);
    }


    [HttpPost("AdicionarCurso", Name = "AdicionarCurso")]
    public async Task<ActionResult<ModuloDTO>> AdicionarCurso([FromBody] CursoDTO Curso)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { mensagem = "Modelo invalido " });
        }

        if (string.IsNullOrWhiteSpace(Curso.Nome))
        {
            return BadRequest(new { mensagem = "O nome do curso nao pode estar vazio " });
        }

        if (string.IsNullOrWhiteSpace(Curso.Professor))
        {
            return BadRequest(new { mensagem = "O nome do professor nao pode estar vazio " });
        }

        var cursoCriar = await _repository.AdicionarCursoAsync(Curso);

        return CreatedAtRoute("BuscarCursoPorId", new { id = Curso.CursoId }, cursoCriar);
    }

    [HttpGet("retornarCursos", Name = "retornarCursos")]
    public async Task<ActionResult<List<CursoDTO>>> RetornarCursos()
    {

        var cursos = await _repository.ObterCursosAsync();

        if (cursos is null || cursos.Count == 0)
        {
            return NotFound(new { mensagem = "Nenhum curso encontrado " });
        }

        return Ok(cursos);
    }

    [HttpDelete("deletarCurso/{id}")]

    public async Task<ActionResult> DeletarCurso([FromRoute] int id)
    {
        var cursoProcurar = await _repository.ObterCursoPorIdAsync(id);

        if (cursoProcurar is null)
        {
            return NotFound(new { mensagem = "Curso nao encontrado " });
        }

        var cursoDeletar = await _repository.DeletarCursoAsync(id);

        return Ok(new { mensagem = $"{cursoProcurar.Nome} deletado com sucesso " });
    }

    [HttpPut("alterarCurso/{id}")]

    public async Task<ActionResult<bool>> AlterarCurso([FromRoute] int id, [FromBody] CursoDTO dTO)
    {
        var buscarCurso = await _repository.ObterCursoPorIdAsync(id);

        if (buscarCurso is null)
        {
            return false;
        }

        await _repository.AlterarCursoAsync(id, dTO);
        return Ok(new { mensagem = "Curso alterado com sucesso " });
    }

}
