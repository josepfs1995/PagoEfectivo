using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PagoEfectivo.Api.Domain.Model;

namespace PagoEfectivo.Api.Infra.Mapping
{
    public class PromocionMap : IEntityTypeConfiguration<Promocion>
    {
        public void Configure(EntityTypeBuilder<Promocion> builder)
        {
            builder.HasKey(x => x.Id_Promocion);

            builder.HasOne(x => x.Estado)
                .WithMany(x => x.Promocion)
                .HasForeignKey(x => x.Id_Estado);
        }
    }
}