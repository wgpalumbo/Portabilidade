namespace Portabilidade.Domain.Entities
{
    public class Agente
    {
        public Agente(string codigo, string nome)
        {
            this.Codigo = codigo;
            this.Nome = nome;

        }
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
    }
}