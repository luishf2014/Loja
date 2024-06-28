namespace loja.models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public String NumeroNotaFiscal { get; set; }
        public Cliente Cliente { get; set; }
        public Produto Produto { get; set; }
        public int QuantidadeVendida { get; set; }
        public double PrecoUnitario
        {
            get
            {
                return Produto != null ? Produto.Preco : 0;
            }
        }
    }
}
