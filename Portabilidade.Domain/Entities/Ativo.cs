namespace Portabilidade.Domain.Entities
{
    public class Ativo
    {
        public Ativo(string codigo, string tipo, decimal quantidade)
        {
            this.Codigo = codigo;
            this.Tipo = tipo;
            this.Quantidade = quantidade;

        }
        public string Codigo { get; private set; }
        public string Tipo { get; private set; }
        public decimal Quantidade { get; private set; }
    }
}