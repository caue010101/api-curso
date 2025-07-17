using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ModuloModelo
{
    [Key]
    public int ModuloId { get; set; }
    public string? Nome { get; set; }

    //chave estrangeira
    public int CursoId { get; set; }
    [JsonIgnore]
    public CursoModelo? Curso { get; set; }
}
