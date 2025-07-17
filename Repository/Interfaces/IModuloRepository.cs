using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IModuloRepository
{
    Task AdicionarModuloAsync(List<ModuloDTO> dTOs);
    Task<ModuloDTO?> BuscarModuloPorIdAsync(int id);
    Task<List<ModuloDTO>> ObterModulosAsync();
    Task<Boolean> DeletarModuloAsync(int id);
    Task<Boolean> AlterarModuloAsync(int id, ModuloDTO dTO);
}
