using loja.models;
using Microsoft.EntityFrameworkCore;

namespace loja.data
{
    public class LojaDbContext : DbContext
    {
        public LojaDbContext(DbContextOptions<LojaDbContext> options)
            : base(options) { }

        public DbSet<Produto> Produtos { get; set; } /* Dbset representa as tabelas do meu Banco de dados */
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Contrato> Contratos {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().HasKey(p => p.id);
            modelBuilder.Entity<Cliente>().HasKey(c => c.Id);
        }
    }
}
