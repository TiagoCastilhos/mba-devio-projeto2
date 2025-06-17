using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace XpertStore.Data.Data;

public class SqliteDbContext : ApplicationDbContext
{
    public SqliteDbContext(IConfiguration configuration) : base(configuration) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(Configuration.GetConnectionString("DefaultConnectionLite"), 
            x => x.MigrationsAssembly("XpertStore.Data"));
    }
}
