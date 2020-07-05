namespace Portabilidade.Domain.Entities
{
    public class Cliente
    {
        public Cliente(string nome, string documentoCpf, string endereco)
        {
            this.Nome = nome;
            this.DocumentoCpf = documentoCpf;
            this.Endereco = endereco;
        
        }
        public string Nome { get; private set; }
        public string DocumentoCpf { get; private set; }
        public string Endereco { get; private set; }

    }
    
}