using System.Linq;
using Microsoft.EntityFrameworkCore;
using PagoEfectivo.Api.Domain.Model;

namespace PagoEfectivo.Api.Infra
{
    public class PagoEfectivoContext : DbContext
    {
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Promocion> Promocion { get; set; }

        public PagoEfectivoContext(DbContextOptions<PagoEfectivoContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties())
                .Where(p => p.ClrType == typeof(string))
                .ToList()
                .ForEach(p => p.SetColumnType("varchar(100)"));
            
            modelBuilder.Seed();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}