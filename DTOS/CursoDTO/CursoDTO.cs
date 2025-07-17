public class CursoDTO
{
    public int CursoId { get; set; }
    public string? Nome { get; set; }
    public string? Professor { get; set; }
    public List<ModuloDTO>? Modulos { get; set; } = new List<ModuloDTO>();
}
