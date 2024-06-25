using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class ClienteService
    {
        private readonly LojaDbContext _dbContext;
        public ClienteService(LojaDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        // Método para consultar todos os Clientes
        public async Task<List<Cliente>> GetAllClientesAsync()
        {
            return await _dbContext.Clientes.ToListAsync();
        }

        // Método para consultar um Cliente a partir do seu Id
        public async Task<Cliente> GetClienteAsync(int id)
        {
            return await _dbContext.Clientes.FindAsync(id);
        }

        // Método para  gravar um novo cliente
        public async Task AddClienteAsync(Cliente cliente)
        {
            _dbContext.Clientes.Add(cliente);
            await _dbContext.SaveChangesAsync();
        }

        // Método para atualizar os dados de um cliente
        public async Task UpdateClienteAsync(Cliente cliente)
        {
            _dbContext.Entry(cliente).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Método para deletar os dados para o cliente
        public async Task DeleteClienteAsync(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if (cliente != null)
            {
                {
                    _dbContext.Clientes.Remove(cliente);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}