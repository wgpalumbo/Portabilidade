using System;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portabilidade.Domain.Entities;

namespace Portabilidade.Tests.Entities
{
    [TestClass]
    public class SolicitacaoTests : AbstractValidator<Solicitacao>
    {
        
        //Variaveis Corretas
        private Cliente cliente = new Cliente("Nome Cliente", "179.506.820-51", "Endereço Cliente Maior que 20");
        private Agente agenteCedente = new Agente("820-6 BB-BI", "12345678");
        private Agente agenteCessionario = new Agente("XP INVESTIMENTOS CCTVM S.A. MATRIZ 3-5", "87654321");
        private Ativo ativo = new Ativo("BBSA3", "Ações", 100);


        [TestMethod]
        public void RetornaTrueQuandoSolicitacaoIsValid()
        {

            var solicitacao = new Solicitacao(Guid.NewGuid(),
                                              Convert.ToDateTime("01/07/2020"),
                                              agenteCedente,
                                              agenteCessionario,
                                              cliente,
                                              10);
            solicitacao.AdicionarAtivo(ativo);

            var validator = new SolicitacaoValidator();
            var validRes = validator.Validate(solicitacao);
            Assert.IsTrue(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoAgenteCedenteIsInvalid()
        {
            var agenteCedente1 = new Agente("", "12345678");
            var solicitacao = new Solicitacao(Guid.NewGuid(),
                                              Convert.ToDateTime("01/07/2020"),
                                              agenteCedente1,
                                              agenteCessionario,
                                              cliente,
                                              10);
            solicitacao.AdicionarAtivo(ativo);
            var validator = new SolicitacaoValidator();
            var validRes = validator.Validate(solicitacao);

            var agenteCedente2 = new Agente("820-6 BB-BI", "");
            solicitacao = new Solicitacao(Guid.NewGuid(),
                                             Convert.ToDateTime("01/07/2020"),
                                             agenteCedente2,
                                             agenteCessionario,
                                             cliente,
                                             10);
            solicitacao.AdicionarAtivo(ativo);
            validator = new SolicitacaoValidator();
            var validRes2 = validator.Validate(solicitacao);

            Assert.IsFalse(validRes.IsValid || validRes2.IsValid);

        }

        [TestMethod]
        public void RetornaFalsoQuandoAgenteCessionarioIsInvalid()
        {
            var agenteCessionario1 = new Agente("", "87654321");
            var solicitacao = new Solicitacao(Guid.NewGuid(),
                                                Convert.ToDateTime("01/07/2020"),
                                                agenteCedente,
                                                agenteCessionario1,
                                                cliente,
                                                10);
            solicitacao.AdicionarAtivo(ativo);
            var validator = new SolicitacaoValidator();
            var validRes = validator.Validate(solicitacao);

            var agenteCessionario2 = new Agente("XP INVESTIMENTOS CCTVM S.A. MATRIZ 3-5", "");
            solicitacao = new Solicitacao(Guid.NewGuid(),
                                              Convert.ToDateTime("01/07/2020"),
                                              agenteCedente,
                                              agenteCessionario2,
                                              cliente,
                                              10);
            solicitacao.AdicionarAtivo(ativo);
            validator = new SolicitacaoValidator();
            var validRes2 = validator.Validate(solicitacao);

            Assert.IsFalse(validRes.IsValid || validRes2.IsValid);

        }

        [TestMethod]
        public void RetornaFalsoQuandoAtivosIsInvalid()
        {

            var solicitacao = new Solicitacao(Guid.NewGuid(),
                                              Convert.ToDateTime("01/07/2020"),
                                              agenteCedente,
                                              agenteCessionario,
                                              cliente,
                                              10);
            //solicitacao.AdicionarAtivo(ativo);

            var validator = new SolicitacaoValidator();
            var validRes = validator.Validate(solicitacao);
            Assert.IsFalse(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoMotivoIsInvalid()
        {

            var solicitacao = new Solicitacao(Guid.NewGuid(),
                                              Convert.ToDateTime("01/07/2020"),
                                              agenteCedente,
                                              agenteCessionario,
                                              cliente,
                                              0);
            solicitacao.AdicionarAtivo(ativo);

            var validator = new SolicitacaoValidator();
            var validRes = validator.Validate(solicitacao);
            Assert.IsFalse(validRes.IsValid);
        }
    }
}
