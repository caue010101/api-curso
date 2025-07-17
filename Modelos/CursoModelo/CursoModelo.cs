using System.ComponentModel.DataAnnotations;

public class CursoModelo
{
    [Key]
    public int CursoId { get; set; }
    public string? Nome { get; set; }
    public string? Professor { get; set; }

    public List<ModuloModelo?> Modulos { get; set; } = new List<ModuloModelo?>();

}
