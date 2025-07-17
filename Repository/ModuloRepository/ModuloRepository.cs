using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ModuloRepository : IModuloRepository
{
    private readonly AppDbContext _context;

    public ModuloRepository(AppDbContext context)
    {
        this._context = context;
    }

    public async Task AdicionarModuloAsync(List<ModuloDTO> dTOs)
    {
        var modulos = dTOs.Select(dto => new ModuloModelo
        {
            ModuloId = dto.ModuloId,
            Nome = dto.Nome,
            CursoId = dto.CursoId
        }).ToList();

        await _context.Modulos.AddRangeAsync(modulos);
        await _context.SaveChangesAsync();
    }

    public async Task<ModuloDTO?> BuscarModuloPorIdAsync(int id)
    {
        var modulo = await _context.Modulos.FindAsync(id);

        if (modulo is null)
        {
            return null;
        }

        return new ModuloDTO
        {
            Nome = modulo.Nome,
            CursoId = modulo.CursoId
        };
    }

    public async Task<List<ModuloDTO>> ObterModulosAsync()
    {
        var modulos = await _context.Modulos.ToListAsync();

        var modulosDto = modulos.Select(m => new ModuloDTO
        {
            Nome = m.Nome,
            CursoId = m.CursoId
        }).ToList();

        return modulosDto;
    }

    public async Task<bool> DeletarModuloAsync(int id)
    {
        var modulos = await _context.Modulos.FindAsync(id);

        if (modulos is null)
        {
            return false;
        }

        _context.Remove(modulos);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AlterarModuloAsync(int id, ModuloDTO dTO)
    {
        var buscarModulo = await _context.Modulos.FindAsync(id);

        if (buscarModulo is null)
        {
            return false;
        }

        buscarModulo.Nome = dTO.Nome;

        await _context.SaveChangesAsync();
        return true;
    }
}
