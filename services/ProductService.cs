using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class ProductService
    {
        private readonly LojaDbContext _dbContext;
        public ProductService(LojaDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        // Método para consultar todos os produtos
        public async Task<List<Produto>> GetAllProdutosAsync()
        {
            return await _dbContext.Produtos.ToListAsync();
        }

        // Método para consultar um produto a partir do seu Id
        public async Task<Produto> GetProdutoAsync(int id)
        {
            return await _dbContext.Produtos.FindAsync(id);
        }

        // Método para  gravar um novo produto
        public async Task AddProductAsync(Produto produto)
        {
            _dbContext.Produtos.Add(produto);
            await _dbContext.SaveChangesAsync();
        }

        // Método para atualizar os dados de um produto
        public async Task UpdateProductAsync(Produto produto)
        {
            _dbContext.Entry(produto).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var produto = await _dbContext.Produtos.FindAsync(id);
            if (produto != null)
            {
                {
                    _dbContext.Produtos.Remove(produto);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}