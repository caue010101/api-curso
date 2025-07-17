using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CursoModelo> Cursos { get; set; }
    public DbSet<ModuloModelo> Modulos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CursoModelo>().ToTable("Cursos");
        modelBuilder.Entity<ModuloModelo>().ToTable("Modulos");
    }
}
