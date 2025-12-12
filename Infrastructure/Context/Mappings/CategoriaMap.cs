using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Mappings;

public class CategoriaMap : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
       builder.ToTable("Categorias");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Finalidade)
            .IsRequired();
    }
}
