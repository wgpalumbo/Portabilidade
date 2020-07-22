using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Portabilidade.Domain.Entities;
using Portabilidade.Domain.Repositories;
using Portabilidade.Service.Util;
using Portabilidade.UI.Controllers;

namespace Portabilidade.Tests.Entities
{

    [TestClass]
    public class SolicitacaoMoqTests
    {
        private Mock<ISqliteRepository<Solicitacao>> mock;

        private string CPF_INVALIDO = "179.506.820-99";
        private string CPF_VALIDO = "179.506.820-51";
        private string CNPJ_VALIDO = "42.630.193/0001-04";
        private string CNPJ_INVALIDO = "42.630.193/0001-99";

        private Agente agenteCedente = new Agente("820-6 BB-BI", "12345678");
        private Agente agenteCessionario = new Agente("XP INVESTIMENTOS CCTVM S.A. MATRIZ 3-5", "87654321");
        private Ativo ativo1 = new Ativo("BBSA3", "Ações", 100);
        private Ativo ativo2 = new Ativo("IPCA+", "Titulo do Tesouro", 300);

        [TestInitialize]
        public void InicializarMockObject()
        {
            mock = new Mock<ISqliteRepository<Solicitacao>>(MockBehavior.Default);

            mock.Setup(s => s.Incluir(It.IsAny<Solicitacao>())).Returns((new ValueTask()));

            mock.Setup(s => s.Excluir(It.IsAny<string>())).Returns((new ValueTask<bool>(true)));

            Cliente clientePF_CPF_Valido = new Cliente("Nome Cliente PF", CPF_VALIDO, "Endereço Cliente Maior que 20");
            var solicitacao = new Solicitacao(Guid.Parse("906d319a-2f3a-4fc4-91aa-ed9699da2b54"),
                                                          Convert.ToDateTime("01/07/2020"),
                                                          agenteCedente,
                                                          agenteCessionario,
                                                          clientePF_CPF_Valido,
                                                          10);
            solicitacao.AdicionarAtivo(ativo1);
            solicitacao.AdicionarAtivo(ativo2);

            mock.Setup(s => s.Obter(It.IsAny<string>())).Returns(new ValueTask<Solicitacao>(solicitacao));


            List<Solicitacao> _local_storage = new List<Solicitacao>();
            _local_storage.Add(solicitacao);

            mock.Setup(s => s.Listar()).Returns(new ValueTask<IEnumerable<Solicitacao>>(_local_storage));

            clientePF_CPF_Valido = null;
            solicitacao = null;
            _local_storage = null;


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
        public void Testar_Incluir_Excluir_Solicitacao()
        {
            //Arrange
            Cliente clientePF_CPF_Valido = new Cliente("Nome Cliente PF", CPF_VALIDO, "Endereço Cliente Maior que 20");
            var localGuid = Guid.NewGuid();
            var solicitacao = new Solicitacao(localGuid,
                                                          Convert.ToDateTime("01/07/2020"),
                                                          agenteCedente,
                                                          agenteCessionario,
                                                          clientePF_CPF_Valido,
                                                          10);
            solicitacao.AdicionarAtivo(ativo1);
            solicitacao.AdicionarAtivo(ativo2);

            //Act
            mock.Object.Incluir(solicitacao);
            mock.Object.Excluir(Convert.ToString(localGuid));
            var resultadoMock = mock.Object.Obter(Convert.ToString(Guid.Parse("906d319a-2f3a-4fc4-91aa-ed9699da2b54")));
            var listaMock = mock.Object.Listar();

            //Assert
            mock.Verify();
        }

        [TestMethod]
        public void Testar_Obter_Solicitacao_Controller()
        {
            //Arrange               
            var controller = new PortabilidadeController(mock.Object);

            //Act            
            var resultadoMockController = controller.ObterPorId(Convert.ToString(Guid.Parse("906d319a-2f3a-4fc4-91aa-ed9699da2b54")));
            var listagemMockController = controller.Listar();
            var result = listagemMockController.Result as OkObjectResult;

            //Assert            
            Assert.IsNotNull(resultadoMockController);
            Assert.IsNotNull(listagemMockController);
            Assert.IsInstanceOfType(resultadoMockController, typeof(Task<IActionResult>));
            Assert.IsInstanceOfType(listagemMockController, typeof(Task<IActionResult>));
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(1, resultadoMockController.Id);
            Assert.AreEqual(2, listagemMockController.Id);

        }


    }
}