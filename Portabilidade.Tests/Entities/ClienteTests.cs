using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portabilidade.Domain.Entities;
using Portabilidade.Service.Util;

namespace Portabilidade.Tests.Entities
{
    [TestClass]
    public class ClienteTests : AbstractValidator<Cliente>
    {

        private ClienteValidator validator;
        

        [TestInitialize]
        public void Init()
        {
            validator = new ClienteValidator();
        }

        [DataTestMethod]
        [DataRow("179.506.820-51")]     //CPF valido
        [DataRow("42.630.193/0001-04")] //CNPJ valido
        public void RetornaTrueQuandoClienteIsValid(string doc)
        {
            var cliente = new Cliente("Nome Cliente PF", doc, "Endereço Cliente Maior que 20");
            var validRes = validator.Validate(cliente);


            Assert.IsTrue(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoClienteIsInvalid()
        {
            //Nome = NULL or Empty
            var cliente = new Cliente("", "179.506.820-51", "Endereço Cliente Maior que 20");
            var validRes = validator.Validate(cliente);
            Assert.IsFalse(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoEnderecoIsInvalid()
        {
            //Endereco menor que 20 caracteres
            var cliente = new Cliente("Nome Cliente", "179.506.820-51", "Endereço Cliente");
            var validRes = validator.Validate(cliente);
            Assert.IsFalse(validRes.IsValid);
        }

        [DataTestMethod]
        [DataRow("")]                   //DocumentoCPF is Null or Empty
        [DataRow("179.506.820-52")]     //DocumentoCPF Mudei um Numero no Digito
        [DataRow("42.630.193/0001-05")] //DocumentoCNPJ Mudei um Numero no Digito
        public void RetornaFalsoQuandoDocumentoIsInvalid(string cpf)
        {
            //DocumentoCPF is Null or Empty
            var cliente = new Cliente("Nome Cliente", cpf, "Endereço Cliente Maior que 20");
            var validRes = validator.Validate(cliente);

            Assert.IsFalse(validRes.IsValid);
        }



        // public void TestMethod1()
        // {
        //     var cliente = new Cliente("", "Numero CPF", "Endereço Cliente");
        //     var agente = new Agente("123", "Nome Agente");
        //     var ativo = new Ativo("123", "ON", 100);


        //     var solicitacao = new Solicitacao(Guid.NewGuid(), Convert.ToDateTime("23/01/2020"), agente, agente, cliente, "123", "321", 10);
        //     solicitacao.AdicionarAtivo(ativo);

        //     var validationResult = new ClienteValidator().Validate(cliente);
        //     foreach (var error in validationResult.Errors)
        //         Console.WriteLine(error.PropertyName, error.ErrorMessage);
        // }


    }
}

