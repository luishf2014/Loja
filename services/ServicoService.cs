using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class ServicoService
    {
        private readonly LojaDbContext _dbContext;

        public ServicoService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para consultar todos os serviços
        public async Task<List<Servico>> GetAllServicosAsync()
        {
            return await _dbContext.Servicos.ToListAsync();
        }

        // Método para consultar um serviço a partir do seu Id
        public async Task<Servico> GetServicoAsync(int id)
        {
            return await _dbContext.Servicos.FindAsync(id);
        }

        // Método para gravar um novo serviço
        public async Task AddServicoAsync(Servico servico)
        {
            _dbContext.Servicos.Add(servico);
            await _dbContext.SaveChangesAsync();
        }

        // Método para atualizar os dados de um serviço
        public async Task UpdateServicoAsync(Servico servico)
        {
            _dbContext.Entry(servico).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // Método para deletar os dados de um serviço
        public async Task DeleteServicoAsync(int id)
        {
            var servico = await _dbContext.Servicos.FindAsync(id);
            if (servico != null)
            {
                _dbContext.Servicos.Remove(servico);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
