using GestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoPedidos.Infrastructure.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<ClienteEntity>
    {
        public void Configure(EntityTypeBuilder<ClienteEntity> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(x => x.IdCliente);

            builder.Property(x => x.IdCliente)
                   .HasColumnName("IdCliente");

            builder.Property(x => x.NomeCompleto)
                   .HasColumnName("NomeCompleto")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Cpf)
                   .HasColumnName("Cpf")
                   .HasMaxLength(11)
                   .IsRequired();
        }
    }
}
