using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class CursoRepository : ICursoRepository
{
    private readonly AppDbContext _context;

    public CursoRepository(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<CursoDTO> AdicionarCursoAsync(CursoDTO dTO)
    {
        var cursoModelo = new CursoModelo
        {
            Nome = dTO.Nome,
            Professor = dTO.Professor,
            Modulos = dTO.Modulos?.Select(m => new ModuloModelo
            {
                Nome = m.Nome,
            }).ToList() ?? new List<ModuloModelo>()
        };

        await _context.Cursos.AddAsync(cursoModelo);
        await _context.SaveChangesAsync();

        return new CursoDTO
        {
            CursoId = cursoModelo.CursoId,
            Nome = cursoModelo.Nome,
            Professor = cursoModelo.Professor,
            Modulos = cursoModelo.Modulos.Select(m => new ModuloDTO
            {
                ModuloId = m.ModuloId,
                Nome = m.Nome,
                CursoId = m.CursoId
            }).ToList()
        };
    }

    public async Task<CursoDTO?> ObterCursoPorIdAsync(int id)
    {
        var curso = await _context.Cursos.Include(m => m.Modulos).FirstOrDefaultAsync(c => c.CursoId == id);

        if (curso is null)
        {
            return null;
        }

        return new CursoDTO
        {
            CursoId = curso.CursoId,
            Nome = curso.Nome,
            Professor = curso.Professor,
            Modulos = curso.Modulos.Select(m => new ModuloDTO
            {
                ModuloId = m.ModuloId,
                Nome = m.Nome,
                CursoId = m.CursoId
            }).ToList()
        };
    }

    public async Task<List<CursoDTO>> ObterCursosAsync()
    {
        var cursos = await _context.Cursos.Include(c => c.Modulos).ToListAsync();

        var cursoDto = cursos.Select(c => new CursoDTO
        {
            CursoId = c.CursoId,
            Nome = c.Nome,
            Professor = c.Professor,
            Modulos = c.Modulos.Select(m => new ModuloDTO
            {
                ModuloId = m.ModuloId,
                Nome = m.Nome,
                CursoId = m.CursoId
            }).ToList()
        }).ToList();

        return cursoDto;
    }

    public async Task<bool> DeletarCursoAsync(int id)
    {
        var cursos = await _context.Cursos.Include(c => c.Modulos).FirstOrDefaultAsync(c => c.CursoId == id); //procura o curso com o id

        if (cursos is null)   //se estiver vazio, retorna falso
        {
            return false;
        }

        if (cursos.Modulos != null && cursos.Modulos.Any())
        {
            _context.RemoveRange(cursos.Modulos);
        }

        _context.Cursos.Remove(cursos);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AlterarCursoAsync(int id, CursoDTO dTO)
    {
        var buscarId = await _context.Cursos.FindAsync(id);

        if (buscarId is null)
        {
            return false;
        }

        buscarId.Nome = dTO.Nome;
        buscarId.Professor = dTO.Professor;

        await _context.SaveChangesAsync();
        return true;
    }
}
