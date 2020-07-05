namespace Portabilidade.Domain.Entities
{
    public class Agente
    {
        public Agente(string instituicao, string codigoInvestidor)
        {
            this.Instituicao = instituicao;
            this.CodigoInvestidor = codigoInvestidor;
        }
        public string Instituicao { get; private set; }
        public string CodigoInvestidor { get; private set; }
    }
}