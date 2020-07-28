using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Portabilidade.Domain.Entities;
using Portabilidade.Domain.Repositories;
using Portabilidade.Infra.Repository;
using Portabilidade.Service.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;

namespace Portabilidade.Tests.Entities
{

    [TestClass]
    public class SolicitacaoMoqTests
    {
        private ISqliteRepository<Solicitacao> mock;

        private string CPF_INVALIDO = "179.506.820-99";
        private string CPF_VALIDO = "179.506.820-51";
        private string CNPJ_VALIDO = "42.630.193/0001-04";
        private string CNPJ_INVALIDO = "42.630.193/0001-99";

        private Agente agenteCedente = new Agente("820-6 BB-BI", "12345678");
        private Agente agenteCessionario = new Agente("XP INVESTIMENTOS CCTVM S.A. MATRIZ 3-5", "87654321");
        private Ativo ativo1 = new Ativo("BBSA3", "Ações", 100);
        private Ativo ativo2 = new Ativo("IPCA+", "Titulo do Tesouro", 300);


        private static Guid localGuid;

        [ClassInitialize]
        public static void ClassSetup(TestContext a)
        {
            localGuid = Guid.NewGuid();
        }

        [TestInitialize]
        public void InicializarMockObject()
        {
            mock = new SqliteSolicitacaoRepository();
        }

        [TestCleanup]
        public void Finalizando()
        {
            mock = null;
            agenteCedente = null;
            agenteCessionario = null;
            ativo1 = null;
            ativo2 = null;
            CPF_INVALIDO = String.Empty;
            CPF_VALIDO = String.Empty;
            CNPJ_VALIDO = String.Empty;
            CNPJ_INVALIDO = String.Empty;

        }

        [TestMethod]
        public void Testar_Cnpj_Valido()
        {
            var mockVirtual = new Mock<IValidarStrategy>();
            mockVirtual.Setup(x => x.IsValid(CNPJ_VALIDO)).Returns(true);

            var mockReal = new Mock<ValidarCnpj>();
            Assert.AreEqual(mockReal.Object.IsValid(CNPJ_VALIDO), mockVirtual.Object.IsValid(CNPJ_VALIDO));

            mockVirtual = null; mockReal = null;
        }

        [TestMethod]
        public void Testar_Cnpj_Invalido()
        {
            var mockVirtual = new Mock<IValidarStrategy>();
            mockVirtual.Setup(x => x.IsValid(CNPJ_INVALIDO)).Returns(false);

            var mockReal = new Mock<ValidarCnpj>();
            Assert.AreEqual(mockReal.Object.IsValid(CNPJ_INVALIDO), mockVirtual.Object.IsValid(CNPJ_INVALIDO));

            mockVirtual = null; mockReal = null;
        }

        [TestMethod]
        public void Testar_Cpf_Invalido()
        {
            var mockVirtual = new Mock<IValidarStrategy>();
            mockVirtual.Setup(x => x.IsValid(CPF_INVALIDO)).Returns(false);

            var mockReal = new Mock<ValidarCpf>();
            Assert.AreEqual(mockReal.Object.IsValid(CPF_INVALIDO), mockVirtual.Object.IsValid(CPF_INVALIDO));

            mockVirtual = null; mockReal = null;
        }

        [TestMethod]
        public void Testar_Cpf_Valido()
        {
            var mockVirtual = new Mock<IValidarStrategy>();
            mockVirtual.Setup(x => x.IsValid(CPF_VALIDO)).Returns(true);

            var mockReal = new Mock<ValidarCpf>();
            Assert.AreEqual(mockReal.Object.IsValid(CPF_VALIDO), mockVirtual.Object.IsValid(CPF_VALIDO));

            mockVirtual = null; mockReal = null;
        }

        [TestMethod]
        public void Testar_Incluir_Solicitacao()
        {
            //Arrange           
            Cliente clientePF_CPF_Valido = new Cliente("Nome Cliente PF", CPF_VALIDO, "Endereço Cliente Maior que 20");
            //var localGuid = Guid.NewGuid();
            var solicitacao = new Solicitacao(localGuid,
                                                          Convert.ToDateTime("01/07/2020"),
                                                          agenteCedente,
                                                          agenteCessionario,
                                                          clientePF_CPF_Valido,
                                                          10);
            solicitacao.AdicionarAtivo(ativo1);
            solicitacao.AdicionarAtivo(ativo2);

            string solicitacaoRetorno = "{'codigoInternoSolicitacao':'" + Convert.ToString(localGuid) + "','dataTransferencia':'2020-07-01T00:00:00','agenteCedente':{'instituicao':'BB BANCO DE INVESTIMENTO S/A - 820','codigoInvestidor':'AA123456'},'agenteCessionario':{'instituicao':'BANK OF AMERICA MERRILL LYNCH - 1817','codigoInvestidor':'13579'},'cliente':{'nome':'Nome Cliente','documentoCpf':'179.506.820-51','endereco':'Endereço Cliente Maior que 20'},'motivo':10,'ativos':[{'codigo':'PETR4','tipo':'Ações','quantidade':100.0},{'codigo':'Debênture','tipo':'Debênture','quantidade':200.0}]}";

            //Act
            bool success = mock.Incluir(JObject.Parse(solicitacaoRetorno)).IsCompletedSuccessfully;
            //mock.Incluir(JObject.Parse(solicitacaoRetorno));

            //Assert            
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void Testar_Obter_Solicitacao()
        {
            //Arrange                                
            Cliente clientePF_CPF_Valido = new Cliente("Nome Cliente PF", CPF_VALIDO, "Endereço Cliente Maior que 20");
            //string solicitacaoRetorno = "{'CodigoInternoSolicitacao':'906d319a-2f3a-4fc4-91aa-ed9699da2b54','DataTransferencia':'2020-07-01T00:00:00','AgenteCedente':{'Instituicao':'BB BANCO DE INVESTIMENTO S/A - 820','CodigoInvestidor':'AA123456'},'AgenteCessionario':{'Instituicao':'BANK OF AMERICA MERRILL LYNCH - 1817','CodigoInvestidor':'13579'},'Cliente':{'Nome':'Nome Cliente','DocumentoCpf':'179.506.820-51','Endereco':'Endereço Cliente Maior que 20'},'Motivo':10,'Ativos':[{'Codigo':'PETR4','Tipo':'Ações','Quantidade':100.0},{'Codigo':'Debênture','Tipo':'Debênture','Quantidade':200.0}]}";

            var solicitacao = new Solicitacao(Guid.Parse("906d319a-2f3a-4fc4-91aa-ed9699da2b54"),
                                                          Convert.ToDateTime("01/07/2020"),
                                                          agenteCedente,
                                                          agenteCessionario,
                                                          clientePF_CPF_Valido,
                                                          10);
            solicitacao.AdicionarAtivo(ativo1);
            solicitacao.AdicionarAtivo(ativo2);

            var mockVirtual = new Mock<ISqliteRepository<Solicitacao>>();
            mockVirtual.Setup(s => s.Obter(It.IsAny<string>())).Returns(new ValueTask<Solicitacao>(solicitacao));

            //Act      
            var resultadoObtido = mock.Obter("906d319a-2f3a-4fc4-91aa-ed9699da2b54");
            var resultadoEsperado = mockVirtual.Object.Obter(Convert.ToString(localGuid));

            //Assert            
            Assert.AreEqual(resultadoObtido.ToString(), resultadoEsperado.ToString());

        }

        [TestMethod]
        public void Testar_Excluir_Solicitacao()
        {

            //Act
            bool success = mock.Excluir(Convert.ToString(localGuid)).IsCompletedSuccessfully;
            //mock.Excluir(Convert.ToString(localGuid));

            //Assert            
            Assert.IsTrue(success);

        }

        [TestMethod]
        public void Testar_Listar_Solicitacao()
        {
            //Arrange
            Cliente clientePF_CPF_Valido = new Cliente("Nome Cliente PF", CPF_VALIDO, "Endereço Cliente Maior que 20");
            var solicitacao = new Solicitacao(Guid.Parse("906d319a-2f3a-4fc4-91aa-ed9699da2b54"),
                                                          Convert.ToDateTime("01/07/2020"),
                                                          agenteCedente,
                                                          agenteCessionario,
                                                          clientePF_CPF_Valido,
                                                          10);
            solicitacao.AdicionarAtivo(ativo1);
            solicitacao.AdicionarAtivo(ativo2);

            List<Solicitacao> _local_lista = new List<Solicitacao>();
            _local_lista.Add(solicitacao);
            string codigoSolicitacaoVirtual = Convert.ToString(_local_lista[0].CodigoInternoSolicitacao);

            //Act            
            List<Solicitacao> listaMock = new List<Solicitacao>((mock.Listar()).Result);
            string codigoSolicitacaoReal = Convert.ToString(listaMock[0].CodigoInternoSolicitacao);
            
            //Assert  
            Assert.AreEqual(codigoSolicitacaoVirtual, codigoSolicitacaoReal);
        }

    }
}