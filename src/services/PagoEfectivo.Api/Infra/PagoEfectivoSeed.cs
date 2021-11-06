using Microsoft.EntityFrameworkCore;
using PagoEfectivo.Api.Domain.Const;
using PagoEfectivo.Api.Domain.Model;

namespace PagoEfectivo.Api.Infra
{
    public static class PagoEfectivoSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estado>()
           .HasData(
               new Estado
               {
                   Id_Estado = EstadoConst.GENERADO,
                   Descripcion = nameof(EstadoConst.GENERADO),
               },
               new Estado
               {
                   Id_Estado = EstadoConst.CANJEADO,
                   Descripcion = nameof(EstadoConst.CANJEADO),
               });
        }
    }
}