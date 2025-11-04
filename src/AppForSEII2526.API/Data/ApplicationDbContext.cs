using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Fabricante> Fabricante { get; set; }
    public DbSet<Oferta> Oferta { get; set; }
    public DbSet<Herramienta> Herramienta { get; set; }
    public DbSet<OfertaItem> OfertaItem { get; set; }
    public DbSet<Reparacion> Reparacion { get; set; }
    public DbSet<ReparacionItem> ReparacionItem { get; set; }
    public DbSet<Comprar> Comprar { get; set; }
    public DbSet<CompraItem> CompraItem { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Oferta>()
            .HasMany(o => o.OfertaItems)
            .WithOne(oi => oi.Oferta)
            .HasForeignKey(oi => oi.OfertaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OfertaItem>(entity =>
        {
            entity.HasKey(e => e.Id); 

            entity.HasOne(oi => oi.Oferta)
                  .WithMany(o => o.OfertaItems)
                  .HasForeignKey(oi => oi.OfertaId);

            entity.HasOne(oi => oi.Herramienta)
                  .WithMany()
                  .HasForeignKey(oi => oi.HerramientaId);
        });
    }
}