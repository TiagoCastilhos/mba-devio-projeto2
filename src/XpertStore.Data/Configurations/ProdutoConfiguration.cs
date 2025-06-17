using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using XpertStore.Data.Models;

namespace XpertStore.Data.Configurations;
public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Nome)
            .IsRequired();

        builder.Property(b => b.Descricao)
            .IsRequired();

        builder.Property(b => b.Preco)
            .IsRequired();

        builder.Property(b => b.Estoque)
            .IsRequired();

        builder.Property(b => b.Categoria)
            .IsRequired();

        builder.Property(b => b.Vendedor)
            .IsRequired();

        builder
            .HasOne(e => e.Categoria)
            .WithMany(e => e.Produtos)
            .HasForeignKey(e => e.CategoriaId)
            .IsRequired();
    }
}
