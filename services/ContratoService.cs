using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using loja.data;
using loja.models;

namespace loja.services
{
    public class ContratoService
    {
        private readonly LojaDbContext _dbContext;

        public ContratoService(LojaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método para registrar um novo contrato
        public async Task RegistrarContratoAsync(Contrato contrato)
        {
            _dbContext.Contratos.Add(contrato);
            await _dbContext.SaveChangesAsync();
        }

        // Método para consultar todos os serviços contratados por um cliente específico
        public async Task<List<Servico>> GetServicosContratadosPorClienteAsync(int clienteId)
        {
            var servicosContratados = await _dbContext
                .Contratos.Where(c => c.ClienteId == clienteId)
                .Include(c => c.Servico)
                .Select(c => c.Servico)
                .ToListAsync();

            return servicosContratados;
        }
    }
}
