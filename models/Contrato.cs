using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loja.models
{
    public class Contrato
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey("Servico")]
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }

        public decimal PrecoCobrado { get; set; }
        public DateTime DataContratacao { get; set; }
    }
}
