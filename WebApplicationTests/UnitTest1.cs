using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using System.Web;

namespace WebApplicationTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RequisitarOperacoesSimultaneamente()
        {
            var CadastrarCidadePorCEP = new TemperaturaController();
            CadastrarCidadePorCEP.CadastrarCidadePorCEP();
            
        }
    }
}
