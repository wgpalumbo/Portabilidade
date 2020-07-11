using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portabilidade.Domain.Entities;

namespace Portabilidade.Tests.Entities
{
    [TestClass]
    public class ClienteTests : AbstractValidator<Cliente>
    {

        [TestMethod]
        public void RetornaTrueQuandoClienteIsValid()
        {
            var cliente = new Cliente("Nome Cliente PF", "179.506.820-51", "Endereço Cliente Maior que 20");
            var validator = new ClienteValidator();
            var validRes = validator.Validate(cliente);

            var cliente2 = new Cliente("Nome Cliente PJ", "42.630.193/0001-04", "Endereço Cliente Maior que 20");
            var validator2 = new ClienteValidator();
            var validRes2 = validator2.Validate(cliente2);


            Assert.IsTrue(validRes.IsValid && validRes2.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoClienteIsInvalid()
        {
            //Nome = NULL or Empty
            var cliente = new Cliente("", "179.506.820-51", "Endereço Cliente Maior que 20");
            var validator = new ClienteValidator();
            var validRes = validator.Validate(cliente);
            Assert.IsFalse(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoEnderecoIsInvalid()
        {
            //Endereco menor que 20 caracteres
            var cliente = new Cliente("Nome Cliente", "179.506.820-51", "Endereço Cliente");
            var validator = new ClienteValidator();
            var validRes = validator.Validate(cliente);
            Assert.IsFalse(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoDocumentoCPFIsInvalid()
        {
            //DocumentoCPF is Null or Empty
            var cliente = new Cliente("Nome Cliente", "", "Endereço Cliente Maior que 20");
            var validator = new ClienteValidator();
            var validRes = validator.Validate(cliente);

            Assert.IsFalse(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoDocumentoCPFIsInvalid2()
        {
            //DocumentoCPF Mudei um Numero no Digito
            var cliente = new Cliente("Nome Cliente PF", "179.506.820-52", "Endereço Cliente Maior que 20");
            var validator = new ClienteValidator();
            var validRes = validator.Validate(cliente);

            Assert.IsFalse(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoDocumentoCNPJInvalid()
        {
            //DocumentoCNPJ Mudei um Numero no Digito
            var cliente = new Cliente("Nome Cliente PJ", "42.630.193/0001-05", "Endereço Cliente Maior que 20");
            var validator = new ClienteValidator();
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

