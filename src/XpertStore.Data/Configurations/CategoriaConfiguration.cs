using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XpertStore.Data.Models;

namespace XpertStore.Data.Configurations;
public class CategoriaoConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Nome).IsRequired();
        builder.Property(b => b.Descricao).IsRequired();
    }
}
