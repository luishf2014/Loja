using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Mvc;

namespace loja.services
{
    public class FornecedorService
    {
        private readonly LojaDbContext _dbContext;
        public FornecedorService(LojaDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        // Método para consultar todos os Fornecedores
        public async Task<List<Fornecedor>> GetAllFornecedorAsync()
        {
            return await _dbContext.Fornecedores.ToListAsync();
        }

        //Método para consultar um fornecedor a partir do seu Id
        public async Task<Fornecedor> GetFornecedorAsync(int id)
        {
            return await _dbContext.Fornecedores.FindAsync(id);
        }

        // Método para gravar um novo fornecedor
        public async Task AddFornecedoresAsync(Fornecedor fornecedor)
        {
            _dbContext.Fornecedores.Add(fornecedor);
            await _dbContext.SaveChangesAsync();
        }

        // Método para atualizar os dados de um fornecedor
        public async Task UpdateFornecedorAsync(Fornecedor fornecedor)
        {
            _dbContext.Entry(fornecedor).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Método para deletar os dados para o cliente
        public async Task DeleteFornecedorAsync(int Id)
        {
            var fornecedor = await _dbContext.Fornecedores.FindAsync(Id);
            if (fornecedor != null)
            {
                {
                    _dbContext.Fornecedores.Remove(fornecedor);
                    await _dbContext.SaveChangesAsync();
                }
            }

        }
    }
}