using System;
using System.Collections.Generic;

namespace Portabilidade.Domain.Entities
{
    public class Solicitacao
    {
        public Solicitacao(Guid codigoInternoSolicitacao, DateTime dataTransferencia, Agente agenteCedente, Agente agenteCessionario, Cliente cliente, int motivo)
        {
            this.CodigoInternoSolicitacao = codigoInternoSolicitacao;
            this.DataTransferencia = dataTransferencia;
            this.AgenteCedente = agenteCedente;
            this.AgenteCessionario = agenteCessionario;
            this.Cliente = cliente;
            this.Motivo = motivo;

        }
        public Guid CodigoInternoSolicitacao { get; private set; }
        public DateTime DataTransferencia { get; private set; }
        public Agente AgenteCedente { get; private set; }
        public Agente AgenteCessionario { get; private set; }
        public Cliente Cliente { get; private set; }
        public int Motivo { get; private set; }
        private List<Ativo> AtivosLocal = new List<Ativo>();
        public IReadOnlyList<Ativo> Ativos { get { return AtivosLocal; } }

        public void AdicionarAtivo(Ativo ativo)
        {
            if (!AtivosLocal.Exists(x => x.Codigo == ativo.Codigo))
            {
                AtivosLocal.Add(ativo);
            }
        }
        // private List<Ativo> Ativos = new List<Ativo>();
        // public List<Ativo> ListarAtivos() => this.Ativos = new List<Ativo>();

    }
}