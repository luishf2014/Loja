using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class VendaService
    {
        private readonly LojaDbContext _dbContext;
        public VendaService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para adicionar uma venda
        public async Task AddVendaAsync(Venda venda)
        {
            // Adicionar a venda ao contexto e salvar
            _dbContext.Vendas.Add(venda);
            await _dbContext.SaveChangesAsync();
        }

        // // Método para consultar vendas detalhadas por produto
        public async Task<List<Venda>> GetVendasDetalhadasPorProdutoAsync(int produtoId)
        {
            var query = await (from venda in _dbContext.Vendas
                               where venda.Produto.id == produtoId
                               select venda).ToListAsync();

            return query;
        }
        // Método para consultar vendas sumarizadas por produto
        public async Task<List<object>> GetVendasSumarizadasPorProdutoAsync(int produtoId)
        {
            return await (from venda in _dbContext.Vendas
                          where venda.Produto.id == produtoId
                          group venda by venda.Produto.Nome into g
                          select new
                          {
                              Produto = g.Key,
                              TotalQuantidadeVendida = g.Sum(v => v.QuantidadeVendida),
                              TotalPrecoVendido = g.Sum(v => v.QuantidadeVendida * v.PrecoUnitario)
                          }).ToListAsync<object>();

        }

        // Método para consultar vendas detalhadas por cliente
        public async Task<List<Venda>> GetVendasDetalhadasPorClienteAsync(int clienteId)
        {
            var query = await (from venda in _dbContext.Vendas
                               where venda.Cliente.Id == clienteId
                               select venda).ToListAsync();

            return query;
        }

        // Método para consultar vendas sumarizadas por cliente
        public async Task<List<object>> GetVendasSumarizadasPorClienteAsync(int clienteId)
        {
            var query = await (from venda in _dbContext.Vendas
                               where venda.Cliente.Id == clienteId
                               group venda by venda.Produto.Nome into g
                               select new
                               {
                                   Produto = g.Key,
                                   TotalQuantidadeVendida = g.Sum(v => v.QuantidadeVendida),
                                   TotalPrecoVendido = g.Sum(v => v.QuantidadeVendida * v.PrecoUnitario)
                               }).ToListAsync<object>();

            return query;
        }
    }
}

