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
            var cliente = new Cliente("Nome Cliente", "Numero CPF", "Endereço Cliente Maior que 20");
            var validator = new ClienteValidator();
            var validRes = validator.Validate(cliente);
            Assert.IsTrue(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoClienteIsInvalid()
        {
            //Nome = NULL or Empty
            var cliente = new Cliente("", "Numero CPF", "Endereço Cliente Maior que 20");
            var validator = new ClienteValidator();
            var validRes = validator.Validate(cliente);
            Assert.IsFalse(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoEnderecoIsInvalid()
        {
            //Endereco menor que 20 caracteres
            var cliente = new Cliente("Nome Cliente", "Numero CPF", "Endereço Cliente");
            var validator = new ClienteValidator();
            var validRes = validator.Validate(cliente);
            Assert.IsFalse(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoDocumentoCPFIsInvalid()
        {
            //DocumentoCPF is Null or Empty
            var cliente = new Cliente("Nome Cliente", "", "Endereço Cliente");
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

