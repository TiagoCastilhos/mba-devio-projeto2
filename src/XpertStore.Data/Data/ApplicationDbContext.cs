using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XpertStore.Data.Models;

namespace XpertStore.Data.Data;

public class ApplicationDbContext : IdentityDbContext
{
    protected readonly IConfiguration Configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
            x => x.MigrationsAssembly("XpertStore.Data"));
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
}
