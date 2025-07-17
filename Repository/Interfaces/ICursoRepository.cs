using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICursoRepository
{
    Task<CursoDTO> AdicionarCursoAsync(CursoDTO curso);
    Task<CursoDTO?> ObterCursoPorIdAsync(int id);
    Task<List<CursoDTO>> ObterCursosAsync();
    Task<Boolean> DeletarCursoAsync(int id);
    Task<Boolean> AlterarCursoAsync(int id, CursoDTO dTO);
}
