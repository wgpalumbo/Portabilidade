using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portabilidade.Domain.Entities;

namespace Portabilidade.Tests.Entities
{
    [TestClass]
    public class ClienteTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var cliente = new Cliente("Nome Cliente", "Numero CPF", "Endereço Cliente");
        }
    }
}
