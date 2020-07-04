using System;
using System.Collections.Generic;

namespace Portabilidade.Domain.Entities
{
    public class Solicitacao
    {
        public Solicitacao(Guid codigoInternoSolicitacao, DateTime dataTransferencia, Agente agenteCedente, Agente agenteCessionario, Cliente cliente, string codigoClienteNoCedente, string codigoClienteNoCessionario, int motivo)
        {
            this.CodigoInternoSolicitacao = codigoInternoSolicitacao;
            this.DataTransferencia = dataTransferencia;
            this.AgenteCedente = agenteCedente;
            this.AgenteCessionario = agenteCessionario;
            this.Cliente = cliente;
            this.CodigoClienteNoCedente = codigoClienteNoCedente;
            this.CodigoClienteNoCessionario = codigoClienteNoCessionario;
            this.Motivo = motivo;
        }
        public Guid CodigoInternoSolicitacao { get; private set; }
        public DateTime DataTransferencia { get; private set; }
        public Agente AgenteCedente { get; private set; }
        public Agente AgenteCessionario { get; private set; }
        public Cliente Cliente { get; private set; }
        public string CodigoClienteNoCedente { get; private set; }
        public string CodigoClienteNoCessionario { get; private set; }
        public int Motivo { get; private set; }
        public List<Ativo> Ativos { get; set; }

        // private List<Ativo> Ativos = new List<Ativo>();
        // public List<Ativo> ListarAtivos() => this.Ativos = new List<Ativo>();

    }
}