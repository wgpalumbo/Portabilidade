using System;
using System.Collections.Generic;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Portabilidade.Domain.Entities;
using Portabilidade.Domain.Repositories;

namespace Portabilidade.Tests.Entities
{

    [TestClass]
    public class ISolicitacaoTests
    {

        private readonly List<Solicitacao> _storage;
        private Mock<ISolicitacaoRepository> mock;


        private const string CPF_INVALIDO = "179.506.820-99";
        private const string CPF_VALIDO = "179.506.820-51";
        private const string CNPJ_VALIDO = "42.630.193/0001-04";
        private const string CNPJ_INVALIDO = "42.630.193/0001-99";

        private Agente agenteCedente = new Agente("820-6 BB-BI", "12345678");
        private Agente agenteCessionario = new Agente("XP INVESTIMENTOS CCTVM S.A. MATRIZ 3-5", "87654321");
        private Ativo ativo1 = new Ativo("BBSA3", "Ações", 100);
        private Ativo ativo2 = new Ativo("IPCA+", "Titulo do Tesouro", 300);

        [TestInitialize]
        public void InicializarMockObject()
        {
            mock = new Mock<ISolicitacaoRepository>(MockBehavior.Strict);

            mock.Setup(s => s.Incluir(It.IsAny<Solicitacao>())).Verifiable();

            mock.Setup(s => s.Alterar(It.IsAny<Guid>(), It.IsAny<Solicitacao>())).Returns(true);

            mock.Setup(s => s.Remover(It.IsAny<Solicitacao>())).Verifiable();


            //Arrange
            Cliente clientePF_CPF_Valido = new Cliente("Nome Cliente PF", CPF_VALIDO, "Endereço Cliente Maior que 20");
            var solicitacao = new Solicitacao(Guid.NewGuid(),
                                                          Convert.ToDateTime("01/07/2020"),
                                                          agenteCedente,
                                                          agenteCessionario,
                                                          clientePF_CPF_Valido,
                                                          10);
            solicitacao.AdicionarAtivo(ativo1);
            solicitacao.AdicionarAtivo(ativo2);

            List<Solicitacao> _local_storage = new List<Solicitacao>();


            // mock.Setup(s => s.ConsultarPendenciasPorCPF(CPF_ERRO_COMUNICACAO))
            //     .Throws(new Exception("Testando erro de comunicação..."));

            // mock.Setup(s => s.ConsultarPendenciasPorCPF(CPF_SEM_PENDENCIAS))
            //     .Returns(() => new List<Pendencia>());

            // List<Pendencia> pendencias = new List<Pendencia>();
            // pendencias.Add(new Pendencia()
            // {
            //     CPF = CPF_INADIMPLENTE,
            //     NomePessoa = "João da Silva",
            //     NomeReclamante = "ACME Comercial LTDA",
            //     DescricaoPendencia = "Cartão de Crédito",
            //     DataPendencia = new DateTime(2015, 02, 14),
            //     VlPendencia = 600.47
            // });
            // mock.Setup(s => s.ConsultarPendenciasPorCPF(CPF_INADIMPLENTE))
            //     .Returns(() => pendencias);
        }

        [TestMethod]
        public void Testar_Incluir_Solicitacao_Cliente_CPF_Valido()
        {
            //Arrange
            Cliente clientePF_CPF_Valido = new Cliente("Nome Cliente PF", CPF_VALIDO, "Endereço Cliente Maior que 20");
            var solicitacao = new Solicitacao(Guid.NewGuid(),
                                                          Convert.ToDateTime("01/07/2020"),
                                                          agenteCedente,
                                                          agenteCessionario,
                                                          clientePF_CPF_Valido,
                                                          10);
            solicitacao.AdicionarAtivo(ativo1);
            solicitacao.AdicionarAtivo(ativo2);








        }

        [TestMethod]
        public void TestarCliente_CPF_Invalido()
        {
            Cliente clientePF_CPF_INvalido = new Cliente("Nome Cliente PF", CPF_INVALIDO, "Endereço Cliente Maior que 20");



        }


        [TestMethod]
        public void IncluirSolicitacao()
        {



        }

        [TestMethod]
        public void ListarSolicitacao()
        {


            mock.Setup(m => m.ListarSolicitacao()).Returns(_storage);


        }

    }
}