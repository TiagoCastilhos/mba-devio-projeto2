using DevXpert.Store.Core.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Store.Core.Data.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasColumnType("VARCHAR(500)");

            builder.Property(c => c.Ativo)
                   .IsRequired()
                   .HasColumnType("BIT");

            builder.HasIndex(c => c.Nome)
                   .HasDatabaseName("IX_NOME_CATEGORIA")
                   .HasFillFactor(80)
                   .IsUnique();

            builder.ToTable("CATEGORIAS");

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.ProdutoId);
        }
    }
}

