using System;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portabilidade.Domain.Entities;

namespace Portabilidade.Tests.Entities
{
    [TestClass]
    public class AtivoTests : AbstractValidator<Ativo>
    {

        [TestMethod]
        public void RetornaTrueQuandoAtivoIsValid()
        {
            var ativo = new Ativo("BBSA3", "Ações", 100);
            var validator = new AtivoValidator();
            var validRes = validator.Validate(ativo);
            Assert.IsTrue(validRes.IsValid);
        }

        [TestMethod]
        public void RetornaFalsoQuandoAtivoIsInvalid()
        {
            var ativo = new Ativo("", "Ações", 100);
            var validator = new AtivoValidator();
            var validRes = validator.Validate(ativo);

            var ativo1 = new Ativo("BBSA3", "", 100);
            var validator1 = new AtivoValidator();
            var validRes1 = validator1.Validate(ativo1);

            var ativo2 = new Ativo("BBSA3", "Ações", 0);
            var validator2 = new AtivoValidator();
            var validRes2 = validator2.Validate(ativo2);

            Assert.IsFalse(validRes.IsValid || validRes1.IsValid || validRes2.IsValid);
        }
        

    }
}

