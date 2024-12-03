using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public required DbSet<Pedido> Pedidos { get; set; }
    public required DbSet<Fornecedor> Fornecedores { get; set; }
}
