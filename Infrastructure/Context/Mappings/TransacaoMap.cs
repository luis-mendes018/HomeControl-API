using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Mappings;

public class TransacaoMap : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {

        builder.ToTable("Transacoes");

        builder.HasKey(t => t.Id);
        builder.Property(x => x.Descricao)
      .IsRequired()
      .HasMaxLength(200);

        builder.Property(x => x.Valor)
            .HasPrecision(18, 2)
            .IsRequired();


        builder.HasOne(x => x.Categoria)
        .WithMany()
        .HasForeignKey(x => x.CategoriaId);
       
        builder.HasOne(x => x.Usuario)
        .WithMany()
        .HasForeignKey(x => x.UsuarioId);

    }
}
