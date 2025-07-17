using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("modulo")]

public class ModuloController : ControllerBase
{
    private readonly IModuloRepository _repository;

    public ModuloController(IModuloRepository repository)
    {
        this._repository = repository;
    }

    [HttpGet("buscarModuloId/{id}")]

    public async Task<ActionResult<ModuloModelo>> BuscarModuloPorId(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { mensagem = "Modelo invalido" });
        }
        var buscarModulo = await _repository.BuscarModuloPorIdAsync(id);

        if (buscarModulo == null)
        {
            return NotFound(new { mensagem = "Modulo nao encontrado " });
        }

        return Ok(buscarModulo);
    }

    [HttpPost("adicionarModulo")]

    public async Task<ActionResult> AdicionarModulo([FromBody] List<ModuloDTO> dtos)
    {
        if (dtos is null || !dtos.Any())
        {
            return NotFound(new { mensagem = "Erro ao adicionar o modulo " });
        }

        await _repository.AdicionarModuloAsync(dtos);
        return Ok(new { mensagem = "Modulo criado com sucesso " });


    }

    [HttpGet("retornarModulos")]

    public async Task<ActionResult<List<ModuloModelo>>> RetornarModulos()
    {
        var obterModulos = await _repository.ObterModulosAsync();

        if (obterModulos == null)
        {
            return NotFound(new { mensagem = "Nenhum modulo encontrado " });
        }

        return Ok(obterModulos);
    }

    [HttpDelete("removerModulo/{id}")]

    public async Task<ActionResult<bool>> RemoverModulo(int id)
    {
        var procurarModulo = await _repository.BuscarModuloPorIdAsync(id);

        if (procurarModulo is null)
        {
            return NotFound(new { mensagem = "Modulo nao encontrado " });
        }

        await _repository.DeletarModuloAsync(id);
        return Ok(new { mensagem = $"{procurarModulo.Nome} foi removido com sucesso " });

    }

    [HttpPut("alterarModulo/{id}")]

    public async Task<ActionResult> AlterarModulo([FromRoute] int id, [FromBody] ModuloDTO dTO)
    {
        var alterarMol = await _repository.AlterarModuloAsync(id, dTO);

        if (!alterarMol)
        {
            return BadRequest(new { mensagem = "Erro ao alterar o modulo" });
        }

        return Ok(new { mensagem = "Modulo alterado com sucesso " });
    }


}
