using DevXpert.Store.Core.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevXpert.Store.Core.Data.Mappings;

public class FavoritoMapping : IEntityTypeConfiguration<Favorito>
{
    public void Configure(EntityTypeBuilder<Favorito> builder)
    {
        builder.HasKey(f => f.Id);

        builder.HasOne(f => f.Cliente)
               .WithMany(c => c.Favoritos)
               .HasForeignKey(f => f.ClienteId);

        builder.HasOne(f => f.Produto)
               .WithMany(p => p.Favoritos)
               .HasForeignKey(f => f.ProdutoId);

        builder.HasIndex(f => new { f.ClienteId, f.ProdutoId })
               .HasDatabaseName("UQ_FAVORITO_CLIENTEID_PRODUTOID")
               .HasFillFactor(80)
               .IsUnique(true);

        builder.ToTable("FAVORITOS");

        builder.Ignore(f => f.ValidationResult);
        builder.Ignore(f => f.Ativo);
    }
}