using Lib.Model;
using Microsoft.EntityFrameworkCore;

namespace Lib.Repository
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conta>().HasKey(c => c.Id);
            modelBuilder.Entity<Conta>().HasIndex(c => new { c.Agencia, c.Numero }).IsUnique();
            modelBuilder.Entity<Conta>().HasMany(c => c.Movimentacoes).WithOne(m => m.Conta);

            modelBuilder.Entity<Movimentacao>().HasKey(m => m.Id);
            modelBuilder.Entity<Movimentacao>().HasOne(m => m.Conta).WithMany(c => c.Movimentacoes);
        }
    }
}
