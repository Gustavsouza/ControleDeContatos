using ControleDeContatos.Data.map;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Data
{
    public class BancoContext : DbContext
    {
        /*Primeiro fazer o construtor 'ctor'*/
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }
        public DbSet<ContatoModel> contatos { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
