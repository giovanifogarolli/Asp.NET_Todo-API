using Microsoft.EntityFrameworkCore;
using TodoAPI.Model;

namespace TodoAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Item>? Itens { get; set; }
    public DbSet<Lista>? Listas { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lista>()
            .HasOne(l => l.User)
            .WithMany(u => u.Listas)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Item>()
            .HasOne(i => i.lista)
            .WithMany(l => l.itens)
            .HasForeignKey(i => i.listaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
